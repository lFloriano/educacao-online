using EducacaoOnline.Conteudo.Application.Queries;
using EducacaoOnline.Core.Communication.Dtos;
using EducacaoOnline.Core.Communication.Gateways;
using EducacaoOnline.Core.Communication.Mediator;

namespace EducacaoOnline.Api.Adapters
{
    public class ConteudoGatewayAdapter : IConteudoGateway
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ConteudoGatewayAdapter(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task<CursoResumoDto?> ObterCursoAsync(Guid cursoId)
        {
            return await _mediatorHandler.EnviarComando<CursoResumoDto?>(new ObterResumoCursoQuery(cursoId));
        }

        public async Task<bool> AulaPertenceAoCursoAsync(Guid cursoId, Guid aulaId)
        {
            return await _mediatorHandler.EnviarComando<bool>(new AulaPertenceAoCursoQuery(cursoId, aulaId));
        }
    }
}