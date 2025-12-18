namespace EducacaoOnline.Conteudo.Application.Dtos
{
    public class CursoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public IEnumerable<AulaDto> Aulas { get; set; } = [];
        public ConteudoProgramaticoDto? ConteudoProgramatico { get; set; } = default;
    }
}
