using AutoMapper;
using EducacaoOnline.PagamentoFaturamento.Application.Dtos;
using EducacaoOnline.PagamentoFaturamento.Domain;

namespace EducacaoOnline.PagamentoFaturamento.Application.Mappings
{
    public class PagamentoMapping : Profile
    {
        public PagamentoMapping()
        {
            // Domain → DTO
            CreateMap<Pagamento, PagamentoDto>()
                .ForMember(dest => dest.DadosCartao, opt => opt.MapFrom(src => new DadosCartaoDto()
                {
                    Numero = src.DadosCartao.Numero,
                    Titular = src.DadosCartao.Titular,
                }))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new StatusPagamentoDto()
                {
                    Status = src.Status.Status,
                }));
        }
    }
}
