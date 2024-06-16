using Core.Models.Common;

namespace Core.RepositoryAbstract
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        Task  AddAsync(T entity);
        void Delete(T entity);
        int Commit();
        Task<int> CommitAsync();
        T Get(Func<T, bool>? func=null);
        List<T> GetAll(Func<T, bool>? func = null);
    }
}
