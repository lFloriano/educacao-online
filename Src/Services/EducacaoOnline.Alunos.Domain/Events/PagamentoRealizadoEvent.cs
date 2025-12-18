using EducacaoOnline.Core.Messages;

namespace EducacaoOnline.Alunos.Domain.Events
{
    public class PagamentoRealizadoEvent : Event
    {
        public PagamentoRealizadoEvent(Guid alunoId, Guid cursoId) : base()
        {
            AlunoId = alunoId;
            CursoId = cursoId;
        }

        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
    }
}
