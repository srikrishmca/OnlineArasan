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
    public class ProductController : ControllerBase
    {

        private ApiDbContext _dbContext;

        public ProductController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _dbContext.Products.ToListAsync();
        }

        // GET api/<ProductController>/5
        [HttpGet("[Action]")]
        public async Task<IEnumerable<Product>> GetProductbyCategoryId(int CategoryId)
        {
            return await _dbContext.Products.Where(i => i.CategoryId == CategoryId).ToListAsync();
        }
        // GET api/<CategoryController>/5
        [HttpGet("[Action]")]
        public async Task<Product> GetProductById(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }
        // GET api/<CategoryController>/5
        [HttpGet("[Action]")]
        public async Task<IEnumerable<Product>> GetProductByIds(string ProductIds)
        {
            try
            {
                List<int> LstProductIds = ProductIds.Split(',').Select(int.Parse).ToList();
                IQueryable<Product> result = from Products in _dbContext.Products
                                             where LstProductIds.Contains(Products.ProductId)
                                             select Products;
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Product>();
            }
        }
        
    }
}
