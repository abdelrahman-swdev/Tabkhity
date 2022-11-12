using System.Linq.Expressions;

namespace Tabkhity.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<List<T>> UpdateRangeAsync(List<T> entities);
        Task<bool> DeleteAsync(T entity);
        Task<List<T>> ListAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<T> FindByIdAsync(string id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> criteria);
        Task<T> FindOneByAsync(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
    }
}
