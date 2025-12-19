using System;
using Xunit;
using EducacaoOnline.Alunos.Domain.ValueObjects;
using EducacaoOnline.Alunos.Domain;

namespace EducacaoOnline.Alunos.Tests.Domain
{
    public class ValueObjectsTests
    {
        [Fact]
        public void HistoricoAprendizado_DefineAulaIdEDataConclusaoUtc()
        {
            var aulaId = Guid.NewGuid();
            var antes = DateTime.UtcNow.AddSeconds(-5);

            var historico = new HistoricoAprendizado(aulaId);

            var depois = DateTime.UtcNow.AddSeconds(5);

            Assert.Equal(aulaId, historico.AulaId);
            Assert.True(historico.DataConclusao >= antes && historico.DataConclusao <= depois);
        }

        [Fact]
        public void Certificado_DefineMatriculaIdEDataCadastro()
        {
            var matriculaId = Guid.NewGuid();
            var certificado = new Certificado(matriculaId);

            Assert.Equal(matriculaId, certificado.MatriculaId);
            Assert.True(certificado.DataCadastro > DateTime.MinValue);
        }
    }
}