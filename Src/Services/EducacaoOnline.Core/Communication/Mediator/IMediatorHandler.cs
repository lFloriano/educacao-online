using EducacaoOnline.Core.Messages;
using MediatR;

namespace EducacaoOnline.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<bool> EnviarComando<T>(T comando) where T : Command;
        Task<TResult> EnviarComando<TResult>(IRequest<TResult> comando);
    }
}
