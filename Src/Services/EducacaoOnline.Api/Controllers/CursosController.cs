using EducacaoOnline.Api.Models.Cursos;
using EducacaoOnline.Conteudo.Application.Commands;
using EducacaoOnline.Conteudo.Application.Dtos;
using EducacaoOnline.Conteudo.Application.Queries;
using EducacaoOnline.Core.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace EducacaoOnline.Api.Controllers
{
    [ApiController]
    [Route("api/cursos")]
    public class CursosController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CursosController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CursoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            var curso = await _mediatorHandler.EnviarComando(new ObterCursoPorIdQuery(id));

            if (curso == null)
                return NotFound("Curso não encontrado");

            return Ok(curso);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<CursoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObterTodos()
        {
            var cursos = await _mediatorHandler.EnviarComando(new ObterTodosOsCursosQuery());

            if (cursos == null || !cursos.Any())
                return NoContent();

            return Ok(cursos);
        }

        [HttpGet("{cursoId:guid}/aulas")]
        [ProducesResponseType(typeof(IEnumerable<AulaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObterAulaPorCursoId([FromRoute] Guid cursoId)
        {
            var aulas = await _mediatorHandler.EnviarComando(new ObterAulasPorCursoIdQuery(cursoId));

            if (aulas == null || !aulas.Any())
                return NoContent();

            return Ok(aulas);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CadastrarCurso([FromBody] CadastrarCursoVm curso)
        {
            var id = await _mediatorHandler.EnviarComando(new CadastrarCursoCommand(curso.Nome, curso.Descricao, curso.NumeroAulas, curso.MaterialDidatico, curso.Valor));
            return Created();
        }

        [HttpPost("{cursoId:guid}/aulas")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CadastrarAula(Guid cursoId, [FromBody] CadastrarAulaVm aula)
        {
            if (cursoId != aula.CursoId)
                return BadRequest("O CursoId da url não corresponde ao CursoId no payload");

            var id = await _mediatorHandler.EnviarComando(new CadastrarAulaCommand(aula.CursoId, aula.Titulo));
            return Created();
        }
    }
}
