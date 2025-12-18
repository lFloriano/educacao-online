using EducacaoOnline.Alunos.Domain.Enums;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Commands
{
    public record FinalizarCursoCommand(Guid AlunoId, Guid CursoId) : IRequest<SituacaoMatricula>;
}
