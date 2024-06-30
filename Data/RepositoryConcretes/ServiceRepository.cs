using Core.Models;
using Core.RepositoryAbstract;
using Data.DAL;

namespace Data.RepositoryConcretes;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(AppDbContext context) : base(context)
    {
    }
}