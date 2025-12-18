using EducacaoOnline.Core.Data;

namespace EducacaoOnline.Conteudo.Domain.Repositories
{
    public interface ICursoRepository : IRepository<Curso>
    {
        Task<Curso?> ObterPorIdAsync(Guid id);
        Task<bool> ExisteCursoComMesmoNomeAsync(string nome);
        Task<IEnumerable<Aula>?> ObterAulasPorCursoIdAsync(Guid id);
        void AdicionarAula(Aula aula);
    }
}
