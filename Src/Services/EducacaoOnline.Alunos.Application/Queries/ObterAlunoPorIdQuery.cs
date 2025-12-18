using EducacaoOnline.Alunos.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Queries
{
    public record ObterAlunoPorIdQuery(Guid id) : IRequest<AlunoDto?>;
}
