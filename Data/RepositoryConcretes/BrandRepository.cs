using Core.Models;
using Core.RepositoryAbstract;
using Data.DAL;

namespace Data.RepositoryConcretes;

public class BrandRepository : GenericRepository<Brand>, IBrandRepository
{
    public BrandRepository(AppDbContext context) : base(context)
    {
    }
}
