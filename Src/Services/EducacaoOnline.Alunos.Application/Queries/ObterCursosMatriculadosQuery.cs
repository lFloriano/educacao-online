using MediatR;

namespace EducacaoOnline.Alunos.Application.Queries
{
    public record ObterCursosMatriculadosQuery(Guid alunoId) : IRequest<IEnumerable<Guid>>;
}
