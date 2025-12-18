using AutoMapper;
using EducacaoOnline.PagamentoFaturamento.Application.Dtos;
using EducacaoOnline.PagamentoFaturamento.Application.Queries;
using EducacaoOnline.PagamentoFaturamento.Domain.Repositories;
using MediatR;

namespace EducacaoOnline.PagamentoFaturamento.Application.Handlers
{
    public class PagamentosQueryHandler : IRequestHandler<ObterPagamentosPorAlunoIdQuery, IEnumerable<PagamentoDto>?>
    {
        readonly IPagamentoRepository _pagamentoRepository;
        readonly IMapper _mapper;

        public PagamentosQueryHandler(IPagamentoRepository pagamentoRepository, IMapper mapper)
        {
            _pagamentoRepository = pagamentoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PagamentoDto>?> Handle(ObterPagamentosPorAlunoIdQuery request, CancellationToken cancellationToken)
        {
            var pagamentos = await _pagamentoRepository.ObterPorAlunoIdAsync(request.AlunoId);
            return _mapper.Map<IEnumerable<PagamentoDto>>(pagamentos);
        }
    }
}
