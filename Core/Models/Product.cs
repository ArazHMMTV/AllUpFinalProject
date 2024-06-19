using Core.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string LongDescription { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public decimal Price { get; set; }
    [Range(0,100)]
    public decimal Discount { get; set; } 

    public string ProductCode { get; set; } =null!;
    [Range(0,5)]
    public decimal Rating { get; set; } = 5;

    public int Quantity { get; set; } 



    public Category Category { get; set; } =null!;
    public int CategoryId { get; set; }

    public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
    public ICollection<ProductImage> ProductImages { get; set; }=new List<ProductImage>();

}
