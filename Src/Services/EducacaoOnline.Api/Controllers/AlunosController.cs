using EducacaoOnline.Alunos.Application.Commands;
using EducacaoOnline.Alunos.Application.Dtos;
using EducacaoOnline.Alunos.Application.Queries;
using EducacaoOnline.Api.Models.Alunos;
using EducacaoOnline.Core.Communication.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EducacaoOnline.Api.Controllers
{
    [ApiController]
    [Authorize]
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
        [SwaggerOperation(Summary = "Obtém aluno por id")]
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
        [SwaggerOperation(Summary = "Obtém aluno por email")]
        public async Task<IActionResult> ObterPorEmail(string email)
        {
            var aluno = await _mediatorHandler.EnviarComando(new ObterAlunoPorEmailQuery(email));

            if (aluno == null)
                return NotFound("Aluno não encontrado");

            return Ok(aluno);
        }


        [HttpGet("{alunoId:guid}/matriculas")]
        [ProducesResponseType(typeof(IEnumerable<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtém as matrículas do aluno")]
        public async Task<IActionResult> ObterMatriculas(Guid alunoId)
        {
            var matriculas = await _mediatorHandler.EnviarComando(new ObterMatriculasQuery(alunoId));

            if (matriculas == null || !matriculas.Any())
                return NoContent();

            return Ok(matriculas);
        }


        [HttpPost("{alunoId:guid}/matriculas")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Realiza matrícula do aluno num curso")]
        public async Task<IActionResult> MatricularAluno(Guid alunoId, [FromBody] NovaMatriculaRequest request)
        {
            if (alunoId != request.AlunoId)
                return BadRequest("O AlunoId da url não corresponde ao AlunoId no payload");

            var matricula = await _mediatorHandler.EnviarComando(new MatricularAlunoCommand(alunoId, request.CursoId));
            return Ok(matricula);
        }

        [HttpPost("{alunoId:guid}/cursos/{cursoId:guid}/aulas/{aulaId:guid}/realizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Permite que o aluno realize as aulas do curso")]
        public async Task<IActionResult> RealizarAula(Guid alunoId, Guid cursoId, Guid aulaId)
        {
            var resultado = await _mediatorHandler.EnviarComando(new RealizarAulaCommand(alunoId, cursoId, aulaId));
            return Ok();
        }


        [HttpPost("{alunoId:guid}/cursos/{cursoId:guid}/finalizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Permite que o aluno finalize o curso após realização de todas as aulas")]
        public async Task<IActionResult> FinalizarCurso(Guid alunoId, Guid cursoId)
        {
            var certificado = await _mediatorHandler.EnviarComando(new FinalizarCursoCommand(alunoId, cursoId));
            return Ok(certificado);
        }
    }
}