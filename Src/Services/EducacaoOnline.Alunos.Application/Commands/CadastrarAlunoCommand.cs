using MediatR;

namespace EducacaoOnline.Alunos.Application.Commands
{
    public record CadastrarAlunoCommand(string? Nome, string? Email, string? ConfirmacaoEmail) : IRequest<Guid>;
}
