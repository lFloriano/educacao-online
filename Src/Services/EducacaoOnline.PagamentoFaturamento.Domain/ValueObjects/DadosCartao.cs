using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.PagamentoFaturamento.Domain.ValueObjects
{
    public class DadosCartao
    {
        public string Titular { get; }
        public string Numero { get; }
        public DateOnly Validade { get; }
        public string CVV { get; }

        protected DadosCartao() { } // EF

        public DadosCartao(string titular, string numero, DateOnly validade, string cvv)
        {
            if (string.IsNullOrWhiteSpace(titular))
                throw new DomainException("Titular do cartão inválido.");

            if (string.IsNullOrWhiteSpace(numero) || numero.Length < 12)
                throw new DomainException("Número do cartão inválido.");

            Titular = titular;
            Numero = numero;
            Validade = validade;
            CVV = cvv;
        }

        protected IEnumerable<object> GetEqualityComponents()
        {
            yield return Numero;
            yield return Validade;
            yield return CVV;
            yield return Titular;
        }
    }

}
