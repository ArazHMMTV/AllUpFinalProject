using Core.Models.Common;
using System.Linq.Expressions;

namespace Core.RepositoryAbstract;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    IQueryable<T> GetAll(params string[] includes);
    IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, params string[] includes);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes);

    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
   
    Task<int> SaveAsync();
}
