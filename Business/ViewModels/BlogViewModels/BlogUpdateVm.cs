using Microsoft.AspNetCore.Http;

namespace Business.ViewModels;

public class BlogUpdateVm
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Author { get; set; } = null!;
    public IFormFile? Image{ get; set; }
    public string? ImagePath{ get; set; }
    public int CategoryId { get; set; }

}
