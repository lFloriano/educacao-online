using EducacaoOnline.Conteudo.Application.Dtos;
using MediatR;

namespace EducacaoOnline.Conteudo.Application.Commands
{
    public record CadastrarCursoCommand(string Nome, string Descricao, int NumeroAulas, string MaterialDidatico, decimal Valor) : IRequest<CursoDto>;
}
