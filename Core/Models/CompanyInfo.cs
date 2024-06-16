using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class CompanyInfo : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
    }
}
