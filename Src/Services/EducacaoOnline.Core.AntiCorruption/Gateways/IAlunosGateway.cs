using EducacaoOnline.Core.Communication.Dtos;

namespace EducacaoOnline.Core.AntiCorruption.Gateways
{
    public interface IAlunosGateway
    {
        Task<AlunoResumoDto?> ObterAlunoAsync(Guid alunoId);
    }
}
