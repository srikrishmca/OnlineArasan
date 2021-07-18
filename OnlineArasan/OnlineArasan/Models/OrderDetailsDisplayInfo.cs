using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineArasan.Models
{
    public class OrderDetailsDisplayInfo
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
        public DateTime OrderDateTime { get; set; }
        public bool Delivered { get; set; }
    }
}
