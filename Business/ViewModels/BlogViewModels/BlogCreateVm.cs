using Microsoft.AspNetCore.Http;

namespace Business.ViewModels;

public class BlogCreateVm
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Author { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;
    public int CategoryId { get; set; }
}
