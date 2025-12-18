namespace EducacaoOnline.Alunos.Domain.ValueObjects
{
    public record HistoricoAprendizado  //Objeto de valor representando aula concluída por um aluno
    {
        public HistoricoAprendizado(Guid aulaId)
        {
            AulaId = aulaId;
            DataConclusao = DateTime.UtcNow;
        }

        public Guid AulaId { get; init; }
        public DateTime DataConclusao { get; init; }
    }
}
