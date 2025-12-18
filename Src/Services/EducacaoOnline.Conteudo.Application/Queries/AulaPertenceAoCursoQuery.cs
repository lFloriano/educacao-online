using MediatR;

namespace EducacaoOnline.Conteudo.Application.Queries
{
    public record AulaPertenceAoCursoQuery(Guid CursoId, Guid AulaId) : IRequest<bool>;
}
