using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineArasan.Models
{
    public class OrderDetails { 
   
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }


    }
}
