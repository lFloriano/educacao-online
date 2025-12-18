using EducacaoOnline.Alunos.Application.Commands;
using EducacaoOnline.Alunos.Application.Dtos;
using EducacaoOnline.Alunos.Application.Queries;
using EducacaoOnline.Alunos.Domain.Enums;
using EducacaoOnline.Api.Models.Alunos;
using EducacaoOnline.Conteudo.Domain;
using EducacaoOnline.Core.Communication.Mediator;
using EducacaoOnline.PagamentoFaturamento.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EducacaoOnline.Api.Controllers
{
    [ApiController]
    [Route("api/alunos")]
    public class AlunosController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AlunosController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var aluno = await _mediatorHandler.EnviarComando(new ObterAlunoPorIdQuery(id));

            if (aluno == null)
                return NotFound("Aluno não encontrado");

            return Ok(aluno);
        }


        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObterPorEmail(string email)
        {
            var aluno = await _mediatorHandler.EnviarComando(new ObterAlunoPorEmailQuery(email));
            return Ok(aluno);
        }


        [HttpGet("{alunoId:guid}/cursos")]
        [ProducesResponseType(typeof(IEnumerable<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObterCursosMatriculados(Guid alunoId)
        {
            var cursos = await _mediatorHandler.EnviarComando(new ObterCursosMatriculadosQuery(alunoId));
            return Ok(cursos ?? new Guid[] { });
        }


        [HttpPost("{alunoId:guid}/matriculas")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> MatricularAluno(Guid alunoId, [FromBody] CursoRequest request)
        {
            if (alunoId != request.AlunoId)
                return BadRequest("O AlunoId da url não corresponde ao AlunoId no payload");

            var matricula = await _mediatorHandler.EnviarComando(new MatricularAlunoCommand(alunoId, request.CursoId));
            return Created();
        }

        [HttpPost("{alunoId:guid}/cursos/{cursoId:guid}/aulas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RealizarAula(Guid alunoId, Guid cursoId, [FromBody] AulaRequest request)
        {
            if (alunoId != request.AlunoId)
                return BadRequest("O AlunoId da url não corresponde ao AlunoId no payload");

            if (cursoId != request.CursoId)
                return BadRequest("O CursoId da url não corresponde ao CursoId no payload");

            var resultado = await _mediatorHandler.EnviarComando(new RealizarAulaCommand(alunoId, cursoId, request.AulaId));
            return Ok(resultado);
        }


        [HttpPost("{alunoId:guid}/cursos/{cursoId:guid}/finalizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> FinalizarCurso(Guid alunoId, Guid cursoId)
        {
            SituacaoMatricula situacao = await _mediatorHandler.EnviarComando(new FinalizarCursoCommand(alunoId, cursoId));
            return Ok(situacao);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CadastrarAluno([FromBody] CadastrarAlunoRequest aluno)
        {
            var id = await _mediatorHandler.EnviarComando(new CadastrarAlunoCommand(aluno.Nome, aluno.Email, aluno.ConfirmacaoEmail));
            return Created();
        }
    }
}