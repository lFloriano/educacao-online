using EducacaoOnline.Conteudo.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Conteudo.Application.Queries
{
    public record ObterAulaPorIdQuery(Guid AulaId) : IRequest<AulaDto?>;
}
