using MediatR;

namespace EducacaoOnline.PagamentoFaturamento.Application.Commands
{
    public record RealizarPagamentoCommand(Guid AlunoId, Guid CursoId, string CartaoTitular, string CartaoNumero, DateOnly CartaoValidade, string CartaoCVV) : IRequest<Guid>;
}
