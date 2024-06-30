namespace Business.ViewModels;

public class SearchVm
{
    public int? CategoryId { get; set; }
    public string SearchValue { get; set; } = null!;
    public string ReturnUrl{ get; set; }=null!;
}
