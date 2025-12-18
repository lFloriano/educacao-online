using EducacaoOnline.Alunos.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Queries
{
    public record ObterMatriculasQuery(Guid alunoId) : IRequest<IEnumerable<MatriculaDto>>;
}
