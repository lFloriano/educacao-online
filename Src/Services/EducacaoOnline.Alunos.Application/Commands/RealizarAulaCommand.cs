using EducacaoOnline.Alunos.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Commands
{
    public record RealizarAulaCommand(Guid AlunoId, Guid CursoId, Guid AulaId) : IRequest<HistoricoAprendizadoDto>;
}
