using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArasanApi.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderTime { get; set; }

        //Delivered when it is true
        public bool IsDelivered { get; set; }

        public DateTime DeliveredTime { get; set; }

        // Active Order to display the item
        public bool IsActive { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
