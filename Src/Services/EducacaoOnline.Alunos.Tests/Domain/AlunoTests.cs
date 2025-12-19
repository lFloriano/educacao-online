using EducacaoOnline.Alunos.Domain;
using EducacaoOnline.Alunos.Domain.Enums;

namespace EducacaoOnline.Alunos.Tests.Domain
{
    public class AlunoTests
    {
        [Fact]
        public void Matricular_DeveGerarMatriculaComSituacaoPendente()
        {
            var alunoId = Guid.NewGuid();
            var aluno = new Aluno(alunoId, "Nome", "email@teste.com");
            var cursoId = Guid.NewGuid();

            var matricula = aluno.Matricular(cursoId);

            Assert.Single(aluno.Matriculas);
            Assert.Equal(cursoId, matricula.CursoId);
            Assert.Equal(alunoId, matricula.AlunoId);
            Assert.Equal(SituacaoMatricula.PendenteDePagamento, matricula.Situacao);
        }

        [Fact]
        public void Matricular_QuandoJaMatriculadoNoCurso_DeveLancarInvalidOperationException()
        {
            var aluno = new Aluno(Guid.NewGuid(), "Nome", "email@teste.com");
            var cursoId = Guid.NewGuid();
            aluno.Matricular(cursoId);

            Assert.Throws<InvalidOperationException>(() => aluno.Matricular(cursoId));
        }

        [Fact]
        public void AtivarMatricula_DeveAlterarSituacaoDaMatriculaParaAtiva()
        {
            var aluno = new Aluno(Guid.NewGuid(), "Nome", "email@teste.com");
            var cursoId = Guid.NewGuid();
            aluno.Matricular(cursoId);

            var matriculaAtivada = aluno.AtivarMatricula(cursoId);

            Assert.Equal(SituacaoMatricula.Ativa, matriculaAtivada.Situacao);
            var matriculaDaColecao = aluno.ObterMatriculaAtivaPorCursoId(cursoId);
            Assert.NotNull(matriculaDaColecao);
            Assert.Equal(SituacaoMatricula.Ativa, matriculaDaColecao!.Situacao);
        }

        [Fact]
        public void AtivarMatricula_QuandoNaoExistePendente_DeveLancarInvalidOperationException()
        {
            var aluno = new Aluno(Guid.NewGuid(), "Nome", "email@teste.com");
            var cursoId = Guid.NewGuid();

            Assert.Throws<InvalidOperationException>(() => aluno.AtivarMatricula(cursoId));
        }

        [Fact]
        public void RealizarAula_DeveGerarHistoricoAprendizado()
        {
            var aluno = new Aluno(Guid.NewGuid(), "Nome", "email@teste.com");
            var cursoId = Guid.NewGuid();
            var aulaId = Guid.NewGuid();

            aluno.Matricular(cursoId);
            aluno.AtivarMatricula(cursoId);

            var historico = aluno.RealizarAula(aulaId, cursoId);

            var matriculaAtiva = aluno.ObterMatriculaAtivaPorCursoId(cursoId);
            Assert.Contains(matriculaAtiva!.HistoricoAprendizado, h => h.AulaId == aulaId);
            Assert.Equal(aulaId, historico.AulaId);
        }

        [Fact]
        public void RealizarAula_QuandoNaoMatriculado_DeveLancarInvalidOperationException()
        {
            var aluno = new Aluno(Guid.NewGuid(), "Nome", "email@teste.com");
            var cursoId = Guid.NewGuid();
            var aulaId = Guid.NewGuid();

            Assert.Throws<InvalidOperationException>(() => aluno.RealizarAula(aulaId, cursoId));
        }

        [Fact]
        public void RealizarAula_QuandoSemMatriculaAtiva_DeveLancarInvalidOperationException()
        {
            var aluno = new Aluno(Guid.NewGuid(), "Nome", "email@teste.com");
            var cursoId = Guid.NewGuid();
            var aulaId = Guid.NewGuid();
            aluno.Matricular(cursoId);

            Assert.Throws<InvalidOperationException>(() => aluno.RealizarAula(aulaId, cursoId));
        }

        [Fact]
        public void FinalizarCurso_DeveConcluirMatriculaEgerarCertificado()
        {
            var aluno = new Aluno(Guid.NewGuid(), "Nome", "email@teste.com");
            var cursoId = Guid.NewGuid();

            aluno.Matricular(cursoId);
            aluno.AtivarMatricula(cursoId);

            var matriculaConcluida = aluno.FinalizarCurso(cursoId);

            Assert.Equal(SituacaoMatricula.Concluida, matriculaConcluida.Situacao);
            Assert.NotNull(matriculaConcluida.Certificado);
            Assert.NotNull(matriculaConcluida.CertificadoId);
        }

        [Fact]
        public void FinalizarCurso_QuandoNaoHaMatriculaAtiva_DeveLancarInvalidOperationException()
        {
            var aluno = new Aluno(Guid.NewGuid(), "Nome", "email@teste.com");
            var cursoId = Guid.NewGuid();
            // Apenas matriculado (pendente) ou sem matricula
            aluno.Matricular(cursoId);

            Assert.Throws<InvalidOperationException>(() => aluno.FinalizarCurso(cursoId));
        }

        [Fact]
        public void ObterCursosMatriculados_ObterCursosConcluidos_DeveCalcularTaxaDeConclusaoDeCursos()
        {
            var aluno = new Aluno(Guid.NewGuid(), "Nome", "email@teste.com");
            var c1 = Guid.NewGuid();
            var c2 = Guid.NewGuid();
            var c3 = Guid.NewGuid();

            aluno.Matricular(c1);
            aluno.Matricular(c2);
            aluno.Matricular(c3);

            aluno.AtivarMatricula(c1);
            aluno.AtivarMatricula(c2);
            // Concluir somente c1 e c2
            aluno.FinalizarCurso(c1);
            aluno.FinalizarCurso(c2);

            var matriculados = aluno.ObterCursosMatriculados().ToList();
            var concluidos = aluno.ObterCursosConcluidos().ToList();
            var taxa = aluno.ObterTaxaDeConclusaoDeCursos();

            Assert.Equal(3, matriculados.Count);
            Assert.Equal(2, concluidos.Count);
            // (2 / 3) * 100 = 66.666... => cast para int => 66
            Assert.Equal(66, taxa);
        }

        [Fact]
        public void CalcularTaxaDeConclusaoDeCursos_QuandoAlunoNaoEstaMatriculado_DeveRetornarZero()
        {
            var aluno = new Aluno(Guid.NewGuid(), "Nome", "email@teste.com");
            var taxaConclusao = aluno.ObterTaxaDeConclusaoDeCursos();

            Assert.Equal(0, taxaConclusao);
        }
    }
}