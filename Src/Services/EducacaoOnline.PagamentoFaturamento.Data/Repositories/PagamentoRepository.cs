using EducacaoOnline.Core.Data;
using EducacaoOnline.PagamentoFaturamento.Domain;
using EducacaoOnline.PagamentoFaturamento.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.PagamentoFaturamento.Data.Repositories
{
    public class PagamentoRepository : Repository<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(PagamentosDbContext context) : base(context)
        {
        }

        public async Task<Pagamento?> ObterPorIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Pagamento>> ObterPorAlunoIdAsync(Guid alunoId)
        {
            return await _dbSet
                .Include(x => x.DadosCartao)
                .Include(x => x.Status)
                .Where(x => x.AlunoId == alunoId)
                .ToListAsync();
        }

        public void RealizarPagamento(Pagamento pagamento)
        {
            _context.Add(pagamento);
        }
    }
}
