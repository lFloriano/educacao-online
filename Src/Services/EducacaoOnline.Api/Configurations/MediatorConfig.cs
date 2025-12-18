using EducacaoOnline.Alunos.Application.Handlers;
using EducacaoOnline.Conteudo.Application.Handlers;
using EducacaoOnline.PagamentoFaturamento.Application.Handlers;

namespace EducacaoOnline.Api.Configurations
{
    public static class MediatorConfig
    {
        public static void AddMediator(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(
                typeof(CursosQueryHandler).Assembly,            //BC Conteúdo
                typeof(AlunosQueryHandler).Assembly,            //BC Alunos
                typeof(PagamentosCommandHandler).Assembly)      //BC Pagamento e Faturamento
            );
        }
    }
}