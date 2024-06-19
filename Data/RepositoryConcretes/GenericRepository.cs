using Core.Models.Common;
using Core.RepositoryAbstract;
using Data.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.RepositoryConcretes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {

        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {

            _context.Set<T>().Remove(entity);

        }



        public IQueryable<T> GetAll(params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }



        public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = _context.Set<T>().Where(expression).AsQueryable();
            foreach (string include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {

            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            T? entity = await query.FirstOrDefaultAsync(expression);
            return entity;
        }


    }
}
