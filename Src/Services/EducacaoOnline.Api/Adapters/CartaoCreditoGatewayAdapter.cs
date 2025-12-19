using EducacaoOnline.Core.AntiCorruption.Gateways;

namespace EducacaoOnline.Api.Adapters
{
    public class CartaoCreditoGatewayAdapter : ICartaoCreditoGateway
    {
        public Task<bool> ValidarCartao(string titular, string numero, string cvv, DateOnly validade)
        {
            return Task.FromResult(SimularValidacao(titular, numero, cvv, validade));
        }

        //Simula validação para evitar implementação de algorítmos complexos fora do escopo deste projeto
        private bool SimularValidacao(string titular, string numero, string cvv, DateOnly validade)
        {
            if (String.IsNullOrEmpty(titular))
                return false;

            if (String.IsNullOrEmpty(numero))
                return false;

            if (String.IsNullOrEmpty(cvv))
                return false;

            if (validade < DateOnly.FromDateTime(DateTime.Now))
                return false;

            return true;
        }
    }
}