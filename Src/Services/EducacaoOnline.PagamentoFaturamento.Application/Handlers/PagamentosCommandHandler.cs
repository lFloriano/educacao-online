using AutoMapper;
using EducacaoOnline.Core.AntiCorruption.Gateways;
using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.PagamentoFaturamento.Application.Commands;
using EducacaoOnline.PagamentoFaturamento.Domain;
using EducacaoOnline.PagamentoFaturamento.Domain.Services;
using EducacaoOnline.PagamentoFaturamento.Domain.ValueObjects;
using MediatR;

namespace EducacaoOnline.PagamentoFaturamento.Application.Handlers
{
    public class PagamentosCommandHandler : IRequestHandler<RealizarPagamentoCommand, Guid>
    {
        private readonly IPagamentoService _pagamentoService;
        private readonly IMapper _mapper;
        private readonly IConteudoGateway _conteudoGateway;
        private readonly IAlunosGateway _alunosGateway;
        private readonly ICartaoCreditoGateway _cartaoCreditoGateway;

        public PagamentosCommandHandler(
            IPagamentoService pagamentoService,
            IMapper mapper,
            IConteudoGateway conteudoGateway,
            IAlunosGateway alunosGateway,
            ICartaoCreditoGateway cartaoCreditoGateway)
        {
            _pagamentoService = pagamentoService;
            _mapper = mapper;
            _conteudoGateway = conteudoGateway;
            _alunosGateway = alunosGateway;
            _cartaoCreditoGateway = cartaoCreditoGateway;
        }

        public async Task<Guid> Handle(RealizarPagamentoCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _alunosGateway.ObterAlunoAsync(request.AlunoId) ??
                throw new NotFoundException("Aluno", request.AlunoId);

            var curso = await _conteudoGateway.ObterCursoAsync(request.CursoId) ??
                throw new NotFoundException("Curso", request.CursoId);

            var existePagamentoConfirmadoAnterior = await _pagamentoService.ExistePagamentoConfirmadoAnteriorAsync(request.AlunoId, request.CursoId);

            if (existePagamentoConfirmadoAnterior)
                throw new InvalidOperationException("O aluno já realizou o pagamento deste curso");

            var cartaoEhValido = await _cartaoCreditoGateway.ValidarCartao(request.CartaoTitular, request.CartaoNumero, request.CartaoCVV, request.CartaoValidade);

            if (!cartaoEhValido)
                throw new InvalidOperationException("Dados do cartão de crédito inválidos");

            var pagamento = new Pagamento(request.AlunoId, request.CursoId, curso.Valor, new DadosCartao(request.CartaoTitular, request.CartaoNumero, request.CartaoValidade, request.CartaoCVV));
            await _pagamentoService.RealizarPagamentoAsync(pagamento);

            return pagamento.Id;
        }
    }
}
