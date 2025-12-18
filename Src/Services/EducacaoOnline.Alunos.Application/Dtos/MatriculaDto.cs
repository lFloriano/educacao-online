using EducacaoOnline.Alunos.Domain.Enums;

namespace EducacaoOnline.Alunos.Application.Dtos
{
    public class MatriculaDto
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public Guid? CertificadoId { get; private set; }
        public SituacaoMatricula Situacao { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public IEnumerable<HistoricoAprendizadoDto> HistoricoAprendizado { get; set; } = Enumerable.Empty<HistoricoAprendizadoDto>();
    }
}
