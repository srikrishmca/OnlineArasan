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

namespace Web.Controllers
{
    public class NewCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
         

        public NewCategoryController(ApplicationDbContext context)
        {
            _context = context;
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

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Images",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            Category category = new Category { CategoryName = CategoryName, CategoryImage = @"wwwroot/Images/"+ file.FileName };
             
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
    }
}
