using EducacaoOnline.Alunos.Application.Mappings;
using EducacaoOnline.Conteudo.Application.Mapping;
using EducacaoOnline.PagamentoFaturamento.Application.Mappings;

namespace EducacaoOnline.Api.Configurations
{
    public static class AutomapperConfig
    {
        public static void AddAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(AlunoMappings).Assembly);
            builder.Services.AddAutoMapper(typeof(CursoMapping).Assembly);
            builder.Services.AddAutoMapper(typeof(PagamentoMapping).Assembly);
        }
    }
}