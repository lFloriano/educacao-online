using EducacaoOnline.Alunos.Domain;
using EducacaoOnline.Alunos.Domain.Repositories;
using EducacaoOnline.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.Alunos.Data.Repositories
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(AlunosDbContext context) : base(context)
        {
        }

        public async Task<Aluno?> ObterPorIdAsync(Guid id)
        {
            return await _dbSet
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.AulasConcluidas)
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.Certificado)
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.HistoricoAprendizado)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Aluno?> ObterPorEmailAsync(string email)
        {
            return await _dbSet
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.AulasConcluidas)
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.Certificado)
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.HistoricoAprendizado)
                .FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());
        }

        public void AdicionarMatricula(Matricula matricula)
        {
            _context.Add(matricula);
        }

        public void AtualizarMatricula(Matricula matricula)
        {
            _context.Update(matricula);
        }
    }
}