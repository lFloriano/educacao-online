using EducacaoOnline.Conteudo.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Conteudo.Application.Queries
{
    public record ObterAulasPorCursoIdQuery(Guid CursoId) : IRequest<IEnumerable<AulaDto?>>;
}
