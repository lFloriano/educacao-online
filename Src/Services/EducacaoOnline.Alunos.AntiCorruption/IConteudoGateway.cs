namespace EducacaoOnline.Alunos.AntiCorruption
{
    public interface IConteudoGateway
    {
        Task<CursoDto> ObterCursoAsync(Guid cursoId);
        Task<bool> AulaPertenceAoCursoAsync(Guid cursoId, Guid aulaId);
    }
}
