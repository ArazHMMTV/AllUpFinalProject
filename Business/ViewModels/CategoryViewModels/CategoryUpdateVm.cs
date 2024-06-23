using Microsoft.AspNetCore.Http;

namespace Business.ViewModels;

public class CategoryUpdateVm
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public IFormFile? Image { get; set; }
    public int? ParentId { get; set; }
}
