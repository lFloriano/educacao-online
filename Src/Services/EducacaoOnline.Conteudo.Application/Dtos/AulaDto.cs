namespace EducacaoOnline.Conteudo.Application.Dtos
{
    public class AulaDto
    {
        public Guid Id { get; set; }
        public Guid CursoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
    }
}
