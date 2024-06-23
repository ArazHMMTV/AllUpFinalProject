using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels;

public class ProductUpdateVm
{

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string LongDescription { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public decimal Price { get; set; }
    [Range(0, 100)]
    public decimal Discount { get; set; }

    public string ProductCode { get; set; } = null!;
    [Range(0, 5)]
    public decimal Rating { get; set; } = 5;

    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public List<int> TagIds { get; set; } = null!;
    public IFormFile? MainImage { get; set; }
    public string? MainImagePath { get; set; }
    public IFormFile? HoverImage { get; set; }
    public string? HoverImagePath { get; set; }

    public List<string>? ImagePaths { get; set; } = new();
    public List<int>? ImageIds { get; set; }= new();
    public List<IFormFile> Images { get; set; } = new();

}
