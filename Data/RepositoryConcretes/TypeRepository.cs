using Core.RepositoryAbstract;
using Data.DAL;

namespace Data.RepositoryConcretes
{
    public class TypeRepository : GenericRepository<Core.Models.Type>, ITypeRepository
    {
        public TypeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
