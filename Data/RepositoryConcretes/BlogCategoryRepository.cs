using Core.Models;
using Core.RepositoryAbstract;
using Data.DAL;

namespace Data.RepositoryConcretes;

public class BlogCategoryRepository : GenericRepository<BlogCategory>, IBlogCategoryRepository
{
    public BlogCategoryRepository(AppDbContext context) : base(context)
    {
    }
}