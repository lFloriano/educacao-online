namespace EducacaoOnline.PagamentoFaturamento.Application.Dtos
{
    public record DadosCartaoDto
    {
        public string Titular { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
    }
}
