using EducacaoOnline.Alunos.Domain.Services;
using MediatR;

namespace EducacaoOnline.Alunos.Domain.Events
{
    public class PagamentoEventHandler : INotificationHandler<PagamentoRealizadoEvent>
    {
        readonly IAlunoService _alunoService;

        public PagamentoEventHandler(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        public async Task Handle(PagamentoRealizadoEvent notification, CancellationToken cancellationToken)
        {
            await _alunoService.AtivarMatriculaAsync(notification.AlunoId, notification.CursoId);
        }
    }
}
