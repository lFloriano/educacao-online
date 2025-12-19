using EducacaoOnline.Core.Data;

namespace EducacaoOnline.Conteudo.Domain.Repositories
{
    public interface ICursoRepository : IRepository<Curso>
    {
        Task<Curso?> ObterPorIdAsync(Guid id);
        Task<bool> ExisteCursoComMesmoNomeAsync(string nome);
        Task<IEnumerable<Aula>?> ObterAulasPorCursoIdAsync(Guid id);
        Task<Aula?> ObterAulaPorIdAsync(Guid id);
        void AdicionarAula(Aula aula);
    }
}
