using EducacaoOnline.Alunos.Application.Commands;
using EducacaoOnline.Alunos.Application.Dtos;
using EducacaoOnline.Alunos.Application.Handlers;
using EducacaoOnline.Alunos.Application.Queries;
using EducacaoOnline.Alunos.Data.Repositories;
using EducacaoOnline.Alunos.Domain.Enums;
using EducacaoOnline.Alunos.Domain.Events;
using EducacaoOnline.Alunos.Domain.Repositories;
using EducacaoOnline.Alunos.Domain.Services;
using EducacaoOnline.Api.Adapters;
using EducacaoOnline.Conteudo.Application.Commands;
using EducacaoOnline.Conteudo.Application.Dtos;
using EducacaoOnline.Conteudo.Application.Handlers;
using EducacaoOnline.Conteudo.Application.Queries;
using EducacaoOnline.Conteudo.Data.Repositories;
using EducacaoOnline.Conteudo.Domain.Repositories;
using EducacaoOnline.Conteudo.Domain.Services;
using EducacaoOnline.Core.Communication.Dtos;
using EducacaoOnline.Core.Communication.Gateways;
using EducacaoOnline.Core.Communication.Mediator;
using EducacaoOnline.PagamentoFaturamento.Application.Commands;
using EducacaoOnline.PagamentoFaturamento.Application.Dtos;
using EducacaoOnline.PagamentoFaturamento.Application.Handlers;
using EducacaoOnline.PagamentoFaturamento.Application.Queries;
using EducacaoOnline.PagamentoFaturamento.Data.Repositories;
using EducacaoOnline.PagamentoFaturamento.Domain.Repositories;
using EducacaoOnline.PagamentoFaturamento.Domain.Services;
using MediatR;

namespace EducacaoOnline.Api.Configurations
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<PagamentoRealizadoEvent>, PagamentoEventHandler>();

            // Communication
            services.AddScoped<IConteudoGateway, ConteudoGatewayAdapter>();
            services.AddScoped<IAlunosGateway, AlunosGatewayAdapter>();

            // BC Alunos
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<IRequestHandler<ObterAlunoResumoQuery, AlunoResumoDto?>, AlunosQueryHandler>();
            services.AddScoped<IRequestHandler<MatricularAlunoCommand, MatriculaCriadaDto>, AlunosCommandHandler>();
            services.AddScoped<IRequestHandler<AtivarMatriculaCommand, SituacaoMatricula>, AlunosCommandHandler>();
            services.AddScoped<IRequestHandler<RealizarAulaCommand, HistoricoAprendizadoDto>, AlunosCommandHandler>();
            services.AddScoped<IRequestHandler<FinalizarCursoCommand, SituacaoMatricula>, AlunosCommandHandler>();
            services.AddScoped<IRequestHandler<ObterMatriculasQuery, IEnumerable<MatriculaDto>>, AlunosQueryHandler>();
            services.AddScoped<IRequestHandler<ObterAlunoPorIdQuery, AlunoDto?>, AlunosQueryHandler>();
            services.AddScoped<IRequestHandler<ObterAlunoPorEmailQuery, AlunoDto?>, AlunosQueryHandler>();

            // BC Conteudo
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<ICursoService, CursoService>();
            services.AddScoped<IRequestHandler<ObterResumoCursoQuery, CursoResumoDto?>, CursosQueryHandler>();
            services.AddScoped<IRequestHandler<ObterCursoPorIdQuery, CursoDto?>, CursosQueryHandler>();
            services.AddScoped<IRequestHandler<ObterTodosOsCursosQuery, IEnumerable<CursoDto>>, CursosQueryHandler>();
            services.AddScoped<IRequestHandler<ObterAulasPorCursoIdQuery, IEnumerable<AulaDto?>>, CursosQueryHandler>();
            services.AddScoped<IRequestHandler<CadastrarCursoCommand, CursoDto>, CursosCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarAulaCommand, AulaDto>, CursosCommandHandler>();
            services.AddScoped<IRequestHandler<AulaPertenceAoCursoQuery, bool>, CursosQueryHandler>();

            //BC Pagamento
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IRequestHandler<RealizarPagamentoCommand, Guid>, PagamentosCommandHandler>();
            services.AddScoped<IRequestHandler<ObterPagamentosPorAlunoIdQuery, IEnumerable<PagamentoDto>?>, PagamentosQueryHandler>();
        }
    }
}
