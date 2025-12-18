namespace EducacaoOnline.Alunos.Application.Dtos
{
    public class AlunoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public IEnumerable<MatriculaDto> Matriculas { get; set; } = Enumerable.Empty<MatriculaDto>();
    }
}
