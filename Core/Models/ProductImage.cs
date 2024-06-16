using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ProductImage : BaseEntity
    {
        public string MainImage { get; set; }
        public string Image { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
