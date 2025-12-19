using EducacaoOnline.Alunos.Domain;
using EducacaoOnline.Alunos.Domain.Enums;

namespace EducacaoOnline.Alunos.Tests.Domain
{
    public class MatriculaTests
    {
        [Fact]
        public void AtivarMatricula_QuandoPendente_AlteraParaAtiva()
        {
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());

            var retorno = matricula.AtivarMatricula();

            Assert.Equal(SituacaoMatricula.Ativa, retorno.Situacao);
            Assert.Equal(SituacaoMatricula.Ativa, matricula.Situacao);
        }

        [Fact]
        public void AtivarMatricula_QuandoNaoPendente_LancaInvalidOperationException()
        {
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());
            matricula.AtivarMatricula();

            Assert.Throws<InvalidOperationException>(() => matricula.AtivarMatricula());
        }

        [Fact]
        public void FinalizarCurso_QuandoAtiva_ConcluiEGeraCertificado()
        {
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());
            matricula.AtivarMatricula();

            var concluida = matricula.FinalizarCurso();

            Assert.Equal(SituacaoMatricula.Concluida, concluida.Situacao);
            Assert.NotNull(concluida.Certificado);
            Assert.Equal(concluida.CertificadoId, concluida.Certificado!.Id);
        }

        [Fact]
        public void FinalizarCurso_QuandoPendente_LancaInvalidOperationException()
        {
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());
            Assert.Throws<InvalidOperationException>(() => matricula.FinalizarCurso());
        }

        [Fact]
        public void FinalizarCurso_QuandoJaConcluida_LancaInvalidOperationException()
        {
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());
            matricula.AtivarMatricula();
            matricula.FinalizarCurso();

            Assert.Throws<InvalidOperationException>(() => matricula.FinalizarCurso());
        }

        [Fact]
        public void RealizarAula_AdicionaHistoricoEPrevineDuplicacao()
        {
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());
            matricula.AtivarMatricula();
            var aulaId = Guid.NewGuid();

            var historico = matricula.RealizarAula(aulaId);

            Assert.Contains(matricula.HistoricoAprendizado, h => h.AulaId == aulaId);
            Assert.Equal(aulaId, historico.AulaId);

            // tentativa duplicada deve falhar
            Assert.Throws<InvalidOperationException>(() => matricula.RealizarAula(aulaId));
        }
    }
}