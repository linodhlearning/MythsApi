using Microsoft.EntityFrameworkCore;
using MythsApi.Application.Interfaces;
using MythsApi.Core.Specification;
using MythsApi.Infrastructure.Data;

namespace MythsApi.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MythsDbContext _context;
        public GenericRepository(MythsDbContext context) => _context = context;

        public async Task<IEnumerable<T>> ListAsync(ISpecification<T> spec)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(ISpecification<T> spec)
        {
            return (await ListAsync(spec)).FirstOrDefault();
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
