using AutoMapper;
using EducacaoOnline.Conteudo.Application.Dtos;
using EducacaoOnline.Conteudo.Domain;
using EducacaoOnline.Conteudo.Domain.ValueObjects;

namespace EducacaoOnline.Conteudo.Application.Mapping
{
    public class CursoMapping : Profile
    {
        public CursoMapping()
        {
            // Domain → DTO
            CreateMap<Curso, CursoDto>();
            CreateMap<Aula, AulaDto>();
            CreateMap<ConteudoProgramatico, ConteudoProgramaticoDto>();
        }
    }
}
