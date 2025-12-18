namespace EducacaoOnline.PagamentoFaturamento.Domain.ValueObjects
{
    public class StatusPagamento
    {
        public string Status { get; }
        public string? MotivoRejeicao { get; }

        public bool EhPendente => Status == "Pendente";
        public bool EhConfirmado => Status == "Confirmado";
        public bool EhRejeitado => Status == "Rejeitado";

        protected StatusPagamento() { } // EF

        private StatusPagamento(string status, string? motivo = null)
        {
            Status = status;
            MotivoRejeicao = motivo;
        }

        public static StatusPagamento Pendente() => new("Pendente");
        public static StatusPagamento Confirmado() => new("Confirmado");
        public static StatusPagamento Rejeitado(string motivo) => new("Rejeitado", motivo);

        protected IEnumerable<object> GetEqualityComponents()
        {
            yield return Status;
            yield return MotivoRejeicao ?? string.Empty;
        }
    }

}
