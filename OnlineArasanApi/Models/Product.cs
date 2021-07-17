using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArasanApi.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }
        public double Discount { get; set; }
        public string ImageName { get; set; }

        public virtual int CategoryId { get; set; }
        public string Description { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Categories { get; set; }
    }
}
