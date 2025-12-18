using EducacaoOnline.Core.Communication.Dtos;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Queries
{
    public record ObterAlunoResumoQuery(Guid CursoId) : IRequest<AlunoResumoDto?>;

}
