using EducacaoOnline.Alunos.Application.Queries;
using EducacaoOnline.Core.Communication.Dtos;
using EducacaoOnline.Core.Communication.Gateways;
using EducacaoOnline.Core.Communication.Mediator;

namespace EducacaoOnline.Api.Adapters
{
    public class AlunosGatewayAdapter : IAlunosGateway
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AlunosGatewayAdapter(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task<AlunoResumoDto?> ObterAlunoAsync(Guid alunoId)
        {
            return await _mediatorHandler.EnviarComando<AlunoResumoDto?>(new ObterAlunoResumoQuery(alunoId));
        }
    }
}