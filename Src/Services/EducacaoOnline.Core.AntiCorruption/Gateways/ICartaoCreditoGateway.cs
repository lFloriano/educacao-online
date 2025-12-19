namespace EducacaoOnline.Core.AntiCorruption.Gateways
{
    public interface ICartaoCreditoGateway
    {
        Task<bool> ValidarCartao(string titular, string numero, string cvv, DateOnly validade);
    }
}
