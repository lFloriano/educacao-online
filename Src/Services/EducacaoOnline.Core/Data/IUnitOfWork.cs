namespace EducacaoOnline.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}