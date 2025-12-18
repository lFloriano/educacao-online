using EducacaoOnline.Core.Data;

namespace EducacaoOnline.Alunos.Domain.Repositories
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<Aluno?> ObterPorIdAsync(Guid id);
        Task<Aluno?> ObterPorEmailAsync(string email);
        void AdicionarMatricula(Matricula matricula);
        void AtualizarMatricula(Matricula matricula);
        void AdicionarCertificado(Certificado certificado);
    }
}
