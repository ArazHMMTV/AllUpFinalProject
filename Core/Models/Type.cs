using Core.Models.Common;

namespace Core.Models
{
    public class Type : BaseEntity
    {
        public string TypeName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
