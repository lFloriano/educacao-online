using EducacaoOnline.Alunos.Domain.Enums;

namespace EducacaoOnline.Alunos.Application.Dtos
{
    public class MatriculaCriadaDto
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public SituacaoMatricula Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
