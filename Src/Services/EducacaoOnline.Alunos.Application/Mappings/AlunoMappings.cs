using AutoMapper;
using EducacaoOnline.Alunos.Application.Dtos;
using EducacaoOnline.Alunos.Domain;
using EducacaoOnline.Alunos.Domain.ValueObjects;

namespace EducacaoOnline.Alunos.Application.Mappings
{
    public class AlunoMappings : Profile
    {
        public AlunoMappings()
        {
            // Domain → DTO
            CreateMap<Aluno, AlunoDto>();
            CreateMap<Matricula, MatriculaCriadaDto>();
            CreateMap<Matricula, MatriculaDto>();
            CreateMap<HistoricoAprendizado, HistoricoAprendizadoDto>();
            CreateMap<Certificado, CertificadoDto>();
        }
    }
}
