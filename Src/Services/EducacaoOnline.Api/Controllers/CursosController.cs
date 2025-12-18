using EducacaoOnline.Api.Models.Cursos;
using EducacaoOnline.Conteudo.Application.Commands;
using EducacaoOnline.Conteudo.Application.Dtos;
using EducacaoOnline.Conteudo.Application.Queries;
using EducacaoOnline.Core.Communication.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Obtém curso por id")]
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
        [SwaggerOperation(Summary = "Lista todos os cursos da plataforma")]
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
        [SwaggerOperation(Summary = "Obtém todas as aulas do curso")]
        public async Task<IActionResult> ObterAulasPorCursoId([FromRoute] Guid cursoId)
        {
            var aulas = await _mediatorHandler.EnviarComando(new ObterAulasPorCursoIdQuery(cursoId));

            if (aulas == null || !aulas.Any())
                return NoContent();

            return Ok(aulas);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Realiza o cadastro de um novo curso")]
        public async Task<IActionResult> CadastrarCurso([FromBody] CadastrarCursoVm curso)
        {
            var id = await _mediatorHandler.EnviarComando(new CadastrarCursoCommand(curso.Nome, curso.Descricao, curso.NumeroAulas, curso.MaterialDidatico, curso.Valor));
            return Created();
        }

        [HttpPost("{cursoId:guid}/aulas")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Realiza o cadastro de uma nova aula")]
        public async Task<IActionResult> CadastrarAula(Guid cursoId, [FromBody] CadastrarAulaVm aula)
        {
            if (cursoId != aula.CursoId)
                return BadRequest("O CursoId da url não corresponde ao CursoId no payload");

            var id = await _mediatorHandler.EnviarComando(new CadastrarAulaCommand(aula.CursoId, aula.Titulo));
            return Created();
        }
    }
}
