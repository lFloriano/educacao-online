using EducacaoOnline.PagamentoFaturamento.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.PagamentoFaturamento.Domain.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;

        public PagamentoService(IPagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task<bool> ExistePagamentoConfirmadoAnteriorAsync(Guid alunoId, Guid CursoId)
        {
            var pagamentos = await _pagamentoRepository.BuscarAsync(x => x.AlunoId == alunoId && x.CursoId == CursoId && x.Status.Status == "Confirmado");
            return pagamentos != null && pagamentos.Any();
        }

        public async Task<Pagamento?> ObterPorIdAsync(Guid id)
        {
            return await _pagamentoRepository.ObterPorIdAsync(id);
        }

        public async Task<Guid> RealizarPagamentoAsync(Pagamento pagamento)
        {
            pagamento.Confirmar();
            _pagamentoRepository.Adicionar(pagamento);

            if (!await _pagamentoRepository.UnitOfWork.Commit())
                throw new DbUpdateException("Falha ao persistir o pagamento.");

            return pagamento.Id;
        }
    }
}
