using EducacaoOnline.Core.Communication.Dtos;

namespace EducacaoOnline.Core.AntiCorruption.Gateways
{
    public interface IConteudoGateway
    {
        Task<CursoResumoDto?> ObterCursoAsync(Guid cursoId);
        Task<bool> AulaPertenceAoCursoAsync(Guid cursoId, Guid aulaId);
    }
}
