namespace EducacaoOnline.PagamentoFaturamento.Application.Dtos
{
    public record PagamentoDto
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public StatusPagamentoDto Status { get; set; }
        public DadosCartaoDto DadosCartao { get; set; }
    }
}
