using Core.Models.Common;

namespace Core.Models;

public class ProductImage : BaseEntity
{
    public string Path { get; set; }= null!;
    public Product Product { get; set; } = null!;
    public int ProductId { get; set; } 
    public bool IsMain { get; set; } = false;
    public bool IsHover { get; set; } = false;
}
