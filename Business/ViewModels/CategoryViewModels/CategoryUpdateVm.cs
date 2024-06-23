namespace Business.ViewModels;

public class CategoryUpdateVm
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
}
