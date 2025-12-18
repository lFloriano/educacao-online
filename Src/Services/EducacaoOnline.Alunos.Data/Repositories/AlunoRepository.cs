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
                    .ThenInclude(m => m.HistoricoAprendizado)
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.Certificado)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Aluno?> ObterPorEmailAsync(string email)
        {
            return await _dbSet
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.HistoricoAprendizado)
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.Certificado)
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

        public void AdicionarCertificado(Certificado certificado)
        {
            _context.Add(certificado);
        }
    }
}