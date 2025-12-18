using EducacaoOnline.Alunos.Domain.Events;
using EducacaoOnline.Api.Models.Pagamentos;
using EducacaoOnline.Core.Communication.Mediator;
using EducacaoOnline.PagamentoFaturamento.Application.Commands;
using EducacaoOnline.PagamentoFaturamento.Application.Dtos;
using EducacaoOnline.PagamentoFaturamento.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducacaoOnline.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/pagamentos")]
    public class PagamentosController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PagamentosController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("/cursos/{cursoId:guid}/alunos/{alunoId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RealizarPagamento([FromRoute] Guid cursoId, [FromRoute] Guid alunoId, [FromBody] PagamentoRequest pagamento)
        {
            if (cursoId != pagamento.CursoId)
                return BadRequest("O CursoId da url não corresponde ao CursoId no payload");

            if (alunoId != pagamento.AlunoId)
                return BadRequest("O AlunoId da url não corresponde ao AlunoId no payload");

            await _mediatorHandler.EnviarComando(new RealizarPagamentoCommand(pagamento.AlunoId, pagamento.CursoId, pagamento.CartaoTitular, pagamento.CartaoNumero, pagamento.CartaoValidade, pagamento.CartaoCVV));
            await _mediatorHandler.PublicarEvento(new PagamentoRealizadoEvent(pagamento.AlunoId, pagamento.CursoId));
            return Ok();
        }

        [HttpGet("alunos/{alunoId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<PagamentoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObterPagamentos(Guid alunoId)
        {
            var pagamentos = await _mediatorHandler.EnviarComando(new ObterPagamentosPorAlunoIdQuery(alunoId));

            if (pagamentos == null || !pagamentos.Any())
                return NoContent();

            return Ok(pagamentos);
        }
    }
}
