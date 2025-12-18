using EducacaoOnline.Conteudo.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Conteudo.Application.Commands
{
    public record CadastrarAulaCommand(Guid cursoId, string Titulo) : IRequest<AulaDto>;

}
