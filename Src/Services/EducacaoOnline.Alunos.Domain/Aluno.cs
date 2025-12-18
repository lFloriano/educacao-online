using EducacaoOnline.Alunos.Domain.Enums;
using EducacaoOnline.Alunos.Domain.ValueObjects;
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.Alunos.Domain
{
    public class Aluno : Entity, IAggregateRoot
    {
        private readonly List<Matricula> _matriculas = new();

        public Aluno(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataCadastro = DateTime.Now;
        }

        public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();
        public string Nome { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime DataCadastro { get; private set; }

        public Matricula Matricular(Guid cursoId)
        {
            if (EstaMatriculadoNoCurso(cursoId))
                throw new InvalidOperationException("Aluno já matriculado no curso");

            var matricula = new Matricula(Id, cursoId);
            _matriculas.Add(matricula);

            return matricula;
        }

        public Matricula AtivarMatricula(Guid cursoId)     //acionado por evento de dominio no ato do pagamento
        {
            var matriculaPendente = ObterMatriculaPorCursoIdESituacao(cursoId, SituacaoMatricula.PendenteDePagamento);

            if (matriculaPendente == null)
                throw new InvalidOperationException("Aluno não possui matrícula pendente de pagamento neste curso");

            return matriculaPendente.AtivarMatricula();
        }

        public HistoricoAprendizado RealizarAula(Guid aulaId, Guid cursoId)
        {
            if (!EstaMatriculadoNoCurso(cursoId))
                throw new InvalidOperationException("Aluno não está matriculado no curso");

            var matriculaAtiva = ObterMatriculaAtivaPorCursoId(cursoId);

            if (matriculaAtiva == null)
                throw new InvalidOperationException("Aluno não possui matrícula ativa no curso");

            return matriculaAtiva.RealizarAula(aulaId);
        }

        private bool EstaMatriculadoNoCurso(Guid? cursoId)
        {
            return _matriculas.Any(x => x.CursoId == cursoId);
        }

        private bool MatriculaEstaPendente(Guid? cursoId)
        {
            return _matriculas.Any(x => x.CursoId == cursoId && x.Situacao == SituacaoMatricula.PendenteDePagamento);
        }

        public Matricula? ObterMatriculaAtivaPorCursoId(Guid cursoId)
        {
            return Matriculas.FirstOrDefault(x => x.CursoId == cursoId && x.Situacao == SituacaoMatricula.Ativa);
        }

        public Matricula? ObterMatriculaPorCursoIdESituacao(Guid cursoId, SituacaoMatricula situacao)
        {
            return Matriculas.FirstOrDefault(x => x.CursoId == cursoId && x.Situacao == situacao);
        }

        public Matricula FinalizarCurso(Guid cursoId)
        {
            var matricula = ObterMatriculaAtivaPorCursoId(cursoId);

            if (matricula == null)
                throw new InvalidOperationException("Aluno não está matriculado no curso");

            return matricula.FinalizarCurso();
        }

        public IEnumerable<Guid> ObterCursosMatriculados()
        {
            return Matriculas.Select(x => x.CursoId);
        }

        public IEnumerable<Guid> ObterCursosConcluidos()
        {
            return Matriculas
                .Where(x => x.Situacao == SituacaoMatricula.Concluida)
                .Select(x => x.CursoId);
        }

        public int ObterTaxaDeConclusaoDeCursos()
        {
            var numeroCursosMatriculados = Matriculas.Count;

            if (numeroCursosMatriculados == 0)
                return 0;

            var numeroCursosConcluidos = ObterCursosConcluidos().Count();
            return (int)((double)numeroCursosConcluidos / numeroCursosMatriculados * 100);

        }
    }
}
