using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArasanApi.Models
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
