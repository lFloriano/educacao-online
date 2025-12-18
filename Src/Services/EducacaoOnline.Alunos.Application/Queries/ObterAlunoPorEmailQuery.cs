using EducacaoOnline.Alunos.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Queries
{
    public record ObterAlunoPorEmailQuery(string email) : IRequest<AlunoDto?>;

}
