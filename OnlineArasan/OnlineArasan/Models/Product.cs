using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineArasan.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }
        public double Discount { get; set; }
        public string ImageName { get; set; }

        public virtual int CategoryId { get; set; }
        public string Description { get; set; }
    }
}
