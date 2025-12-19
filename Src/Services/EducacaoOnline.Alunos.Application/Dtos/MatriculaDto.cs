using EducacaoOnline.Alunos.Domain.Enums;

namespace EducacaoOnline.Alunos.Application.Dtos
{
    public class MatriculaDto
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public Guid? CertificadoId { get; set; }
        public SituacaoMatricula Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public IEnumerable<HistoricoAprendizadoDto> HistoricoAprendizado { get; set; } = Enumerable.Empty<HistoricoAprendizadoDto>();
    }
}
