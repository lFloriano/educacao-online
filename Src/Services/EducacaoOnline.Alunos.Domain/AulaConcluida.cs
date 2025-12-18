using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.Alunos.Domain
{
    public class AulaConcluida  //entidade associativa entre aula e matricula
    {
        public AulaConcluida(Guid aulaId, Guid matriculaId)
        {
            AulaId = aulaId;
            MatriculaId = matriculaId;
        }

        public Guid AulaId { get; private set; }
        public Guid MatriculaId { get; private set; }
        public Matricula? Matricula { get; set; }
    }
}
