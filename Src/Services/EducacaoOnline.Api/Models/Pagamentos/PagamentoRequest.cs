namespace EducacaoOnline.Api.Models.Pagamentos
{
    public record PagamentoRequest(Guid AlunoId, Guid CursoId, string CartaoTitular, string CartaoNumero, DateOnly CartaoValidade, string CartaoCVV);
}
