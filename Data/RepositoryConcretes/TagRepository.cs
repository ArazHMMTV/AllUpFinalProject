using Core.Models;
using Core.RepositoryAbstract;
using Data.DAL;

namespace Data.RepositoryConcretes;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context)
    {
    }
}
