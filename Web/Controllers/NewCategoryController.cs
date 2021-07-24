using OnlineArasan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Microsoft.Extensions.Configuration;
using Web.Models;

namespace Web.Controllers
{
    public class NewCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IOptions<MySettings> _configuration;

        public NewCategoryController(ApplicationDbContext context, IOptions<MySettings> configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string CategoryName)
        {
            if (string.IsNullOrEmpty(CategoryName))
            {
                return Content("Please enter CategoryName");
            }

            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(),                         
                        _configuration.Value.ImagePath,
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            Category category = new Category { CategoryName = CategoryName, CategoryImage = _configuration.Value.RetrieveImagePath + file.FileName };
             
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
    }
}
