using Core.Models;
using Core.RepositoryAbstract;
using Data.DAL;

namespace Data.RepositoryConcretes;

public class TestmonialRepository : GenericRepository<Testimonial>, ITestimonialRepository
{
    public TestmonialRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
