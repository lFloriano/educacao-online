using EducacaoOnline.Alunos.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Commands
{
    public record MatricularAlunoCommand(Guid AlunoId, Guid CursoId) : IRequest<MatriculaCriadaDto>;
}
