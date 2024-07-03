using Core.Models;

namespace Business.ViewModels;

public class HomeVm
{
    public List<Slider> Sliders { get; set; } = new();
    public List<Product> BestProducts { get; set; } = new();
    public List<Product> NewArrival { get; set; } = new();
    public List<Product> BestsellerProducts { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public List<Blog> Blogs { get; set; } = new();

}
