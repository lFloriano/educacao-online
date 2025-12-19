using AutoMapper;
using EducacaoOnline.Alunos.Application.Dtos;
using EducacaoOnline.Alunos.Domain;
using EducacaoOnline.Alunos.Domain.Services;
using EducacaoOnline.Api.Models.Alunos;
using EducacaoOnline.Core.Enums;
using EducacaoOnline.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
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
        private readonly IMapper _mapper;

        public UsuariosController(UserManager<IdentityUser> userManager, IAlunoService alunoService, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _alunoService = alunoService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Realiza o cadastro de um usuário com o perfil de aluno")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarAlunoRequest request)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(request.Email);

            if (usuarioExistente != null)
                ModelState.AddModelError("Email", $"Já existe usuário cadastrado com o email {request.Email}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoUsuario = await AddUsuario(request);
            await AssociarUsuarioNaRole(novoUsuario);
            var alunoCriado = await AddAluno(novoUsuario.Id.NormalizeGuid(), request);

            return CreatedAtAction(
                actionName: nameof(AlunosController.ObterPorId),
                controllerName: "Alunos",
                routeValues: new { id = alunoCriado.Id },
                value: alunoCriado);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtém token JWT para um usuário já cadastrado")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await ObterTokenUsuario(request);

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

        private async Task<AlunoDto> AddAluno(Guid id, CadastrarAlunoRequest request)
        {
            var alunoCriado = await _alunoService.CadastrarAlunoAsync(new Aluno(id, request.Nome, request.Email));
            return _mapper.Map<AlunoDto>(alunoCriado);
        }

        private string ObterDescricaoErros(IdentityResult identityResult)
        {
            var erros = identityResult.Errors.Select(x => x.Description);
            return string.Join(", ", erros);
        }

        private async Task<string> ObterTokenUsuario(LoginRequest login)
        {
            var usuario = await _userManager.FindByEmailAsync(login.Email) ??
                throw new InvalidOperationException("Email ou senha inválidos");

            var senhaEhValida = await _userManager.CheckPasswordAsync(usuario, login.Senha);

            if (!senhaEhValida)
                throw new InvalidOperationException("Email ou senha inválidos");

            return await GerarJwt(usuario);
        }

        private async Task<string> GerarJwt(IdentityUser usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario?.UserName)
            };

            var roles = await _userManager.GetRolesAsync(usuario);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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
