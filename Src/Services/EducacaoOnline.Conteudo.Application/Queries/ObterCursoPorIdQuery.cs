using EducacaoOnline.Conteudo.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Conteudo.Application.Queries
{
    public record ObterCursoPorIdQuery(Guid CursoId) : IRequest<CursoDto?>;
}
