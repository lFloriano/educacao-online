namespace EducacaoOnline.Api.Models.Cursos
{
    public class CadastrarCursoVm
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string MaterialDidatico { get; set; } = string.Empty;
        public int NumeroAulas { get; set; }
        public decimal Valor { get; set; }
    }
}
