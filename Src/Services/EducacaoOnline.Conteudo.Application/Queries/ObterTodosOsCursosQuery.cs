using EducacaoOnline.Conteudo.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Conteudo.Application.Queries
{
    public record ObterTodosOsCursosQuery() : IRequest<IEnumerable<CursoDto>>;
}
