using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineArasanApi.Data;
using OnlineArasanApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArasanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Filters.APIKeyAuth]
    public class CustomerController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public CustomerController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<Customer> GetCustomerById(int CustomerId)
        {
            return await _dbContext.Customers.AsNoTracking().Where(i => i.CustomerId == CustomerId).FirstOrDefaultAsync();
        }
        [HttpPost("[action]")]
        public async Task<int> InsertUpdateCustomer(Customer customer)
        {
            try
            {
                if (customer.CustomerId > 0)
                {
                    _dbContext.Customers.Update(customer);
                }
                else
                {
                    _dbContext.Customers.Add(customer);
                }
                await _dbContext.SaveChangesAsync();
                return customer.CustomerId;
            }
            catch (System.Exception ex)
            {
                return 0;
                throw new System.Exception(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<Customer> GetCustomerByNumber(string CustomerNumber)
        {

            return await _dbContext.Customers.AsNoTracking().Where(i => i.MobileNo == CustomerNumber).FirstOrDefaultAsync();
        }

    }
}
