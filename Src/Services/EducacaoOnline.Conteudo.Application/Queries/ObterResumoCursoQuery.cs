using EducacaoOnline.Core.Communication.Dtos;
using MediatR;

namespace EducacaoOnline.Conteudo.Application.Queries
{
    public record ObterResumoCursoQuery(Guid CursoId) : IRequest<CursoResumoDto?>;
}
