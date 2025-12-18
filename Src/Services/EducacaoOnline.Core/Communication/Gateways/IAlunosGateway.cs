using EducacaoOnline.Core.Communication.Dtos;

namespace EducacaoOnline.Core.Communication.Gateways
{
    public interface IAlunosGateway
    {
        Task<AlunoResumoDto?> ObterAlunoAsync(Guid alunoId);
    }
}
