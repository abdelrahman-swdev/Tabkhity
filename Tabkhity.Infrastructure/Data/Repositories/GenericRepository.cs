using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tabkhity.Core.Interfaces;

namespace Tabkhity.Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TabhkityDbContext _context;
        private DbSet<T> _entitySet;
        public GenericRepository(TabhkityDbContext context)
        {
            _context = context;
            _entitySet = _context.Set<T>();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _entitySet.FindAsync(id);
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _entitySet.AsNoTracking().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _entitySet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            await _entitySet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return true;
        }

        public async Task<List<T>> UpdateRangeAsync(List<T> entities)
        {
            _entitySet.UpdateRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _entitySet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _entitySet.FindAsync(id);
        }

        public async Task<T> FindByIdAsync(string id)
        {
            return await _entitySet.FindAsync(id);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> criteria)
        {
            var items = _entitySet.AsNoTracking().AsQueryable();
            return items.Where(criteria);
        }

        public async Task<T> FindOneByAsync(Expression<Func<T, bool>> criteria)
        {
            var items = _entitySet.AsNoTracking().AsQueryable();
            return await items.Where(criteria).FirstOrDefaultAsync();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            var items = _entitySet.AsNoTracking().AsQueryable();
            return items.Where(criteria).CountAsync();
        }
    }
}
