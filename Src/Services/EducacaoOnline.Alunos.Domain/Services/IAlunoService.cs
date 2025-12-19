using EducacaoOnline.Alunos.Domain.ValueObjects;

namespace EducacaoOnline.Alunos.Domain.Services
{
    /// <summary>
    /// Contrato para serviços de domínio da raiz de agregação <see cref="Aluno"/>.
    /// Responsável por coordenar operações de domínio que envolvem a entidade Aluno.
    /// </summary>
    public interface IAlunoService
    {
        /// <summary>
        /// Recupera um aluno pelo identificador.
        /// </summary>
        Task<Aluno?> ObterPorIdAsync(Guid id);

        /// <summary>
        /// Recupera um aluno pelo email.
        /// </summary>
        Task<Aluno?> ObterPorEmailAsync(string email);

        /// <summary>
        /// Cria/Adiciona um novo aluno no sistema.
        /// </summary>
        Task<Aluno> CadastrarAlunoAsync(Aluno aluno);

        /// <summary>
        /// Matricula um aluno em um curso.
        /// </summary>
        Task<Matricula> MatricularAsync(Guid alunoId, Guid cursoId);

        /// <summary>
        /// Registra o pagamento da matrícula do aluno no curso, ativando-a.
        /// </summary>
        Task<Matricula> AtivarMatriculaAsync(Guid alunoId, Guid cursoId);

        /// <summary>
        /// Marca conclusão de uma aula para o aluno em um curso.
        /// </summary>
        Task<HistoricoAprendizado> RealizarAulaAsync(Guid alunoId, Guid cursoId, Guid aulaId);

        /// <summary>
        /// Finaliza o curso para o aluno (gera certificado/histórico na matrícula).
        /// </summary>
        Task<Matricula> FinalizarCursoAsync(Guid alunoId, Guid cursoId);

        /// <summary>
        /// Retorna os ids dos cursos em que o aluno está matriculado.
        /// </summary>
        Task<IEnumerable<Guid>> ObterCursosMatriculadosAsync(Guid alunoId);

        /// <summary>
        /// Retorna os ids dos cursos concluídos pelo aluno.
        /// </summary>
        Task<IEnumerable<Guid>> ObterCursosConcluidosAsync(Guid alunoId);

        /// <summary>
        /// Retorna a taxa de conclusão (em porcentagem) do aluno.
        /// </summary>
        Task<int> ObterTaxaDeConclusaoDeCursosAsync(Guid alunoId);
    }
}