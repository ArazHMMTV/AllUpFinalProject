using Microsoft.AspNetCore.Http;

namespace Business.ViewModels;

public class SliderUpdateVm
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Subtitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile? Image { get; set; } 
    public string? ImagePath { get; set; } 
}
