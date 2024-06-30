using Core.Models;
using Core.RepositoryAbstract;
using Data.DAL;

namespace Data.RepositoryConcretes;

public class SliderRepository : GenericRepository<Slider>, ISliderRepository
{
    public SliderRepository(AppDbContext context) : base(context)
    {
    }
}
