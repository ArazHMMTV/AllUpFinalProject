using Microsoft.AspNetCore.Http;

namespace Business.ViewModels;

public class SliderCreateVm
{
    public string Title { get; set; } = null!;
    public string Subtitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;
}
