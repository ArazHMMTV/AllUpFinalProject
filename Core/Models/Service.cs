using Core.Models.Common;

namespace Core.Models;

public class Service:BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
}
