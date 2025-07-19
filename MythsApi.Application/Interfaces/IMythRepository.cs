using MythsApi.Core.Entities;
using MythsApi.Core.Specification;
namespace MythsApi.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> ListAsync(ISpecification<T> spec);
        Task<T?> GetAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }

    public interface IMythRepository : IGenericRepository<Myth> { }
}
