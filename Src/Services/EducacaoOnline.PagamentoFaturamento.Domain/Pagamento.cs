using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.PagamentoFaturamento.Domain.ValueObjects;

namespace EducacaoOnline.PagamentoFaturamento.Domain
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }

        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public DadosCartao DadosCartao { get; private set; }
        public StatusPagamento Status { get; private set; }

        protected Pagamento() { } // EF

        public Pagamento(Guid alunoId, Guid cursoId, decimal valor, DadosCartao dadosCartao)
        {
            if (valor <= 0)
                throw new DomainException("O valor do pagamento deve ser maior que zero.");

            AlunoId = alunoId;
            CursoId = cursoId;
            Valor = valor;
            DadosCartao = dadosCartao;
            Status = StatusPagamento.Pendente();
            DataCadastro = DateTime.Now;
        }

        public void Confirmar()
        {
            if (!Status.EhPendente)
                throw new DomainException("O pagamento já foi processado.");

            Status = StatusPagamento.Confirmado();
        }

        public void Rejeitar(string motivo)
        {
            if (!Status.EhPendente)
                throw new DomainException("O pagamento já foi processado.");

            Status = StatusPagamento.Rejeitado(motivo: motivo);
        }
    }
}
