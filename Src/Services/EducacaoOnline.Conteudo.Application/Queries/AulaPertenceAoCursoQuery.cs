using MediatR;

namespace EducacaoOnline.Conteudo.Application.Queries
{
    public record AulaPertenceAoCursoQuery(Guid CursoId, Guid aulaId) : IRequest<bool>;
}
