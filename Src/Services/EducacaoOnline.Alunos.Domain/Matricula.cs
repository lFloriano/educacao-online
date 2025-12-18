using EducacaoOnline.Alunos.Domain.Enums;
using EducacaoOnline.Alunos.Domain.ValueObjects;
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.Alunos.Domain
{
    public class Matricula : Entity
    {
        private readonly List<AulaConcluida> _aulasConcluidas = new();

        public Matricula(Guid alunoId, Guid cursoId)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            Situacao = SituacaoMatricula.PendenteDePagamento;
            DataCadastro = DateTime.Now;
        }

        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public Guid? CertificadoId { get; private set; }
        public SituacaoMatricula Situacao { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public IReadOnlyCollection<AulaConcluida> AulasConcluidas => _aulasConcluidas.AsReadOnly();

        public Aluno? Aluno { get; private set; }
        public Certificado? Certificado { get; private set; }
        public HistoricoAprendizado? HistoricoAprendizado { get; private set; }

        public Matricula AtivarMatricula()
        {
            if (Situacao != SituacaoMatricula.PendenteDePagamento)
                throw new InvalidOperationException("O pagamento já foi realizado");

            Situacao = SituacaoMatricula.Ativa;
            return this;
        }

        public Matricula FinalizarCurso()
        {
            if (Situacao == SituacaoMatricula.PendenteDePagamento)
                throw new InvalidOperationException("A matrícula se encontra pendente de pagamento");

            if (Situacao == SituacaoMatricula.Concluida)
                throw new InvalidOperationException("A matrícula já está concluída");

            Situacao = SituacaoMatricula.Concluida;
            Certificado = GerarCertificadoDeConclusao();
            CertificadoId = Certificado.Id;
            HistoricoAprendizado = GerarHistoricoDeAprendizado();

            return this;
        }

        private HistoricoAprendizado GerarHistoricoDeAprendizado()
        {
            return new HistoricoAprendizado(Id);
        }

        private Certificado GerarCertificadoDeConclusao()
        {
            if (Situacao != SituacaoMatricula.Concluida)
                throw new InvalidOperationException("Não é possível gerar certificado para curso não concluído");

            return new Certificado(Id);
        }

        public AulaConcluida RealizarAula(Guid aulaId)
        {
            if (AulasConcluidas.Any(x => x.AulaId == aulaId))
                throw new InvalidOperationException("Aula já concluída anteriormente");

            var aulaConcluida = new AulaConcluida(aulaId, Id);
            _aulasConcluidas.Add(aulaConcluida);
            return aulaConcluida;
        }
    }
}
