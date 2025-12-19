using EducacaoOnline.Alunos.Domain;

namespace EducacaoOnline.Alunos.Tests.Domain
{
    public class CertificadoTests
    {
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