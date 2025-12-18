namespace EducacaoOnline.PagamentoFaturamento.Domain.Services
{
    public interface IPagamentoService
    {
        /// <summary>
        /// Recupera um Pagamento pelo identificador.
        /// </summary>
        Task<Pagamento?> ObterPorIdAsync(Guid id);

        /// <summary>
        /// Verifica se o aluno já pagou um curso.
        /// </summary>
        Task<bool> ExistePagamentoConfirmadoAnteriorAsync(Guid alunoId, Guid CursoId);

        /// <summary>
        /// Cria/Adiciona um novo Pagamento no sistema.
        /// </summary>
        Task<Guid> RealizarPagamentoAsync(Pagamento Pagamento);
    }
}
