using EducacaoOnline.Conteudo.Domain;
using EducacaoOnline.Conteudo.Domain.Repositories;
using EducacaoOnline.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.Conteudo.Data.Repositories
{
    public class CursoRepository : Repository<Curso>, ICursoRepository
    {
        public CursoRepository(CursosDbContext context) : base(context)
        {
        }

        public async Task<Curso?> ObterPorIdAsync(Guid id)
        {
            return await _dbSet
                .Include(a => a.Aulas)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> ExisteCursoComMesmoNomeAsync(string nome)
        {
            if (String.IsNullOrEmpty(nome)) return false;

            return await _dbSet.AnyAsync(a => a.Nome.ToLower() == nome.ToLower());
        }

        public async Task<IEnumerable<Aula>?> ObterAulasPorCursoIdAsync(Guid id)
        {
            var curso = await _dbSet
                .Include(x => x.Aulas)
                .FirstOrDefaultAsync(x => x.Id == id);

            return curso?.Aulas;
        }

        public async Task<Aula?> ObterAulaPorIdAsync(Guid id)
        {
            var aula = await ((CursosDbContext)_context).Aulas.FirstOrDefaultAsync(x => x.Id == id);
            return aula;
        }

        public void AdicionarAula(Aula aula)
        {
            _context.Add(aula);
        }
    }
}
