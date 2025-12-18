using EducacaoOnline.Core.Data;

namespace EducacaoOnline.PagamentoFaturamento.Domain.Repositories
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Task<Pagamento?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Pagamento>> ObterPorAlunoIdAsync(Guid alunoId);
        void RealizarPagamento(Pagamento pagamento);
    }
}
