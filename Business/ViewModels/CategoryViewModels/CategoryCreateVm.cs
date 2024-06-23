using Microsoft.AspNetCore.Http;

namespace Business.ViewModels;

public class CategoryCreateVm
{
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
    public IFormFile  Image { get; set; }=null!;
}
