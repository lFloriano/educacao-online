using AutoMapper;
using EducacaoOnline.Alunos.Application.Dtos;
using EducacaoOnline.Alunos.Domain;

namespace EducacaoOnline.Alunos.Application.Mappings
{
    public class AlunoMappings : Profile
    {
        public AlunoMappings()
        {
            // Domain → DTO
            CreateMap<Aluno, AlunoDto>();
            CreateMap<Matricula, MatriculaCriadaDto>();
        }
    }
}
