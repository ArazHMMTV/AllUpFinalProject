using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } // hp
        public string Description { get; set; } // dwnden
        public decimal Price { get; set; } // 2000

        public bool Availability { get; set; } //  true
        public string ProductCode { get; set; } // ijdednju

        public int Quantity { get; set; } // 200
        public int Discount { get; set; } // 0


        public string ImageUrl { get; set; }  // dmekmk

        public Category Category { get; set; } // notebbok => type?
        public int CategoryId { get; set; }

        public ICollection<Type>? Types { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }

    }
}
