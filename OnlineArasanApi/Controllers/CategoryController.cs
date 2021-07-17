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
    public class CategoryController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public CategoryController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IEnumerable<Category>> Index()
        {
            return await _dbContext.Categories.ToListAsync();
        }
        
    }
}
