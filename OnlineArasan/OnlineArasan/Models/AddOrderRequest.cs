using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineArasan.Models
{
    public class AddOrderRequest
    {
        public Order Order { get; set; }
        public List<OrderDetails> LstOrderDetails { get; set; }
    }
    public class OrderResponse
    {
        public List<Order> LstOrder { get; set; }
        public List<OrderDetails> LstOrderDetails { get; set; }
    }
}
