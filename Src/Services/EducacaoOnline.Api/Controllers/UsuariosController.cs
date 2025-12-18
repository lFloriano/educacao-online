using EducacaoOnline.Alunos.Domain;
using EducacaoOnline.Alunos.Domain.Services;
using EducacaoOnline.Api.Models.Alunos;
using EducacaoOnline.Core.Enums;
using EducacaoOnline.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EducacaoOnline.Api.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [AllowAnonymous]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAlunoService _alunoService;
        private readonly IConfiguration _configuration;

        public UsuariosController(UserManager<IdentityUser> userManager, IAlunoService alunoService, IConfiguration configuration)
        {
            _userManager = userManager;
            _alunoService = alunoService;
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarAlunoRequest request)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(request.Email);

            if (usuarioExistente != null)
                ModelState.AddModelError("Email", $"Já existe usuário cadastrado com o email {request.Email}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoUsuario = await AddUsuario(request);
            await AssociarUsuarioNaRole(novoUsuario);
            await AddAluno(novoUsuario.Id.NormalizeGuid(), request);

            return Created();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var aluno = await _alunoService.ObterPorEmailAsync(request.Email);

            if (aluno == null)
                return Unauthorized("Credencias inválidas");

            var token = await ObterUsuario(request);

            if (String.IsNullOrEmpty(token))
                return BadRequest("Erro ao gerar token para o usuário");

            return Ok(token);
        }

        private async Task<IdentityUser> AddUsuario(CadastrarAlunoRequest request)
        {
            var novoUsuario = new IdentityUser()
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var resultadoCadastro = await _userManager.CreateAsync(novoUsuario, request.Senha);

            if (!resultadoCadastro.Succeeded)
            {
                var descricaoErros = ObterDescricaoErros(resultadoCadastro);
                throw new InvalidOperationException($"Erro ao realizar cadastro do usuário: {descricaoErros}");
            }

            return novoUsuario;
        }

        private async Task AssociarUsuarioNaRole(IdentityUser identityUser)
        {
            var descricaoRole = TipoUsuario.Aluno.GetDescription();
            var resultadoCadastro = await _userManager.AddToRoleAsync(identityUser, descricaoRole);

            if (!resultadoCadastro.Succeeded)
            {
                await _userManager.DeleteAsync(identityUser);
                var descricaoErros = ObterDescricaoErros(resultadoCadastro);
                throw new InvalidOperationException($"Erro ao atribuir role ao usuário: {descricaoErros}");
            }
        }

        private async Task AddAluno(Guid id, CadastrarAlunoRequest request)
        {
            var aluno = new Aluno(id, request.Nome, request.Email);
            await _alunoService.CadastrarAlunoAsync(aluno);
        }

        private string ObterDescricaoErros(IdentityResult identityResult)
        {
            var erros = identityResult.Errors.Select(x => x.Description);
            return string.Join(", ", erros);
        }

        private async Task<string> ObterUsuario(LoginRequest login)
        {
            var usuario = await _userManager.FindByEmailAsync(login.Email) ??
                throw new InvalidOperationException("Email ou senha inválidos");

            var senhaEhValida = await _userManager.CheckPasswordAsync(usuario, login.Senha);

            if (!senhaEhValida)
                throw new InvalidOperationException("Email ou senha inválidos");

            var aluno = await _alunoService.ObterPorEmailAsync(login.Email) ??
                throw new InvalidOperationException($"Aluno não encontrado na base com o email {login.Email}");

            return GerarJwt(usuario, aluno);
        }

        private string GerarJwt(IdentityUser usuario, Aluno aluno)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, aluno?.Nome?? usuario?.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiresInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
