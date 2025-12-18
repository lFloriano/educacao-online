using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.Alunos.Domain
{
    public class Certificado : Entity
    {
        public Certificado(Guid matriculaId)
        {
            MatriculaId = matriculaId;
            DataCadastro = DateTime.Now;
        }

        public Guid MatriculaId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Matricula? Matricula { get; private set; }
    }
}
