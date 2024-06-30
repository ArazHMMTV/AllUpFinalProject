using Core.Models.Common;

namespace Core.Models;

public class Slider:BaseEntity
{
    public string Title { get; set; } = null!;
    public string Subtitle { get; set; } = null!;
    public string Description{ get; set; } = null!;
    public string ImagePath{ get; set; } = null!;
}
