using EducacaoOnline.Alunos.Domain.Enums;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Commands
{
    public record AtivarMatriculaCommand(Guid AlunoId, Guid CursoId): IRequest<SituacaoMatricula>;
}
