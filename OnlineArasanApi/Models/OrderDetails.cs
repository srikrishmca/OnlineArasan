using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArasanApi.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
        
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
