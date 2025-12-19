using EducacaoOnline.Alunos.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Commands
{
    public record FinalizarCursoCommand(Guid AlunoId, Guid CursoId) : IRequest<CertificadoDto>;
}
