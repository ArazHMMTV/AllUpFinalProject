using Core.Models;
using Core.RepositoryAbstract;
using Data.DAL;

namespace Data.RepositoryConcretes
{
    public class CompanyInfoRepository : GenericRepository<CompanyInfo>, ICompanyInfoRepository
    {
        public CompanyInfoRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
