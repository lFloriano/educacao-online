using EducacaoOnline.Alunos.Domain.ValueObjects;

namespace EducacaoOnline.Alunos.Tests.Domain
{
    public class HistoricoAprendizadoTests
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
    }
}