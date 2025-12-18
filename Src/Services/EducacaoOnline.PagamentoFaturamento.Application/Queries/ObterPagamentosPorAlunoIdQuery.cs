using EducacaoOnline.PagamentoFaturamento.Application.Dtos;
using MediatR;

namespace EducacaoOnline.PagamentoFaturamento.Application.Queries
{
    public record ObterPagamentosPorAlunoIdQuery(Guid AlunoId) : IRequest<IEnumerable<PagamentoDto>?>;
}
