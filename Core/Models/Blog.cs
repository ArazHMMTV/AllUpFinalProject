using Core.Models.Common;

namespace Core.Models;

public class Blog : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedTime { get; set; } 
    public string Author { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public BlogCategory BlogCategory { get; set; } = null!;
    public int BlogCategoryId { get; set; } 

}