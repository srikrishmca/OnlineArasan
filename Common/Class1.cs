using OnlineArasan.Models;
using System;
using System.ComponentModel;


namespace Common
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        
        [DisplayName("Order Number")]
        public int OrderId { get; set; }
        
        public Customer CustomerDetail { get; set; }

         

        public int ProductId { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Discount { get; set; }
        public string OrderTime { get; set; }
        public int Quantity { get; set; }

        [DisplayName("Delivered Time")]
        public string OrderDeliveredTime { get; set; }
        //Order.DeliveredTime
        public bool OrderIsDelivery { get; set; }
        public Product ProductDetails { get; set; }
        public Order SingleOrderDetail { get; set; }
        public Category CategoryDetails { get; set; }
        //Order.IsDeliver


    }
}
