using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineArasan.Models
{
   public class Order
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderTime { get; set; }

        //Delivered when it is true
        public bool IsDelivered { get; set; }

        public DateTime DeliveredTime { get; set; }

        // Active Order to display the item
        public bool IsActive { get; set; }

    }
}
