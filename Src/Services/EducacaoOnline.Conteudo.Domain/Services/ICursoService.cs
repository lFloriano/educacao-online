namespace EducacaoOnline.Conteudo.Domain.Services
{
    public interface ICursoService
    {
        Task<Curso?> ObterPorIdAsync(Guid id);
        Task<Curso> CadastrarCursoAsync(Curso curso);
        Task<Aula> CadastrarAulaAsync(Aula aula);
        Task<IEnumerable<Curso>> ObterTodosAsync();
        Task<IEnumerable<Aula>?> ObterAulasPorCursoIdAsync(Guid id);
    }
}
