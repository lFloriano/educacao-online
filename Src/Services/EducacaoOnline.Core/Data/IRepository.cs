using EducacaoOnline.Core.DomainObjects;
using System.Linq.Expressions;

namespace EducacaoOnline.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<T>> ObterTodosAsync();
        Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicate);
        void Adicionar(T entity);
        void Atualizar(T entity);
        void Remover(T entity);
    }
}