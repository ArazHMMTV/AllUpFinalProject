using Core.Models;

namespace Business.ViewModels;

public class BlogVm
{
    public List<Blog> Blogs { get; set; } = new();
    public List<BlogCategory> BlogCategories { get; set; } = new();
}
