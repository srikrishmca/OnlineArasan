using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArasanApi.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string MobileNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
    }
}
