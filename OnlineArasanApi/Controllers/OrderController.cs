using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineArasanApi.Data;
using OnlineArasanApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArasanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Filters.APIKeyAuth]
    public class OrderController : Controller
    {
        private ApiDbContext _dbContext;

        public OrderController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            return await _dbContext.Orders.ToListAsync();
        }
        // POST api/<CategoryController>
        [HttpPost("[action]")]
        public async Task<int> AddOrder(AddOrderRequest _Request)
        {
            try
            {
                _dbContext.Orders.Add(_Request.Order);
                await _dbContext.SaveChangesAsync();
                int OrderId = _Request.Order.OrderId;
                if (_Request.LstOrderDetails != null)
                {
                    foreach (OrderDetails item in _Request.LstOrderDetails)
                    {
                        item.OrderId = OrderId;
                        _dbContext.OrderDetails.Add(item);
                    }
                }
                await _dbContext.SaveChangesAsync();
                return OrderId;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        [HttpGet("[action]")]
        public async Task<OrderResponse> GetOrderByCustomerId(int CustomerId)
        {
            OrderResponse _orderResponse = new OrderResponse
            {
                LstOrder = await _dbContext.Orders.Where(i => i.CustomerId == CustomerId && i.IsActive).ToListAsync()
            };

            List<int> SelectedOrderIds = _orderResponse.LstOrder.Select(c => c.OrderId).Distinct().ToList();

            IQueryable<OrderDetails> result = from orderDetails in _dbContext.OrderDetails
                                              where SelectedOrderIds.Contains(orderDetails.OrderId)
                                              select orderDetails;

            _orderResponse.LstOrderDetails = new List<OrderDetails>(result);
            return _orderResponse;
        }
    }
}
