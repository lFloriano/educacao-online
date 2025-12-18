namespace EducacaoOnline.Alunos.Domain.ValueObjects
{
    public class HistoricoAprendizado   //é uma projeção consolidando cursos finalizados
    {
        public HistoricoAprendizado(Guid matriculaId)
        {
            MatriculaId = matriculaId;
            DataConclusao = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public Guid MatriculaId { get; private set; }
        public DateTime DataConclusao { get; private set; }

        public Matricula? Matricula { get; private set; }
    }
}
