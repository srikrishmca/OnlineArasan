using OnlineArasan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;

namespace Web.Controllers
{
    public class NewProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            List<Category> categories = new List<Category>();
            categories = (from c in _context.Categories select c).ToList();
            categories.Insert(0, new Category { CategoryId = 0, CategoryName = "--Select Category--" });
            ViewBag.message = categories;

            //Categories categories = new Categories();
            //categories.CategoriesList = _context.Category.ToList<Category>();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string ProductName,string Price, string Discount, string Imagename, 
            string CategorySelectedId, string Description)
        {
            if (string.IsNullOrEmpty(ProductName))
            {
                return Content("Please enter ProductName");
            }

            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Images/Products",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            Product product = new Product {
                ProductName = ProductName,
                Price = double.Parse(Price),
                Discount = double.Parse(Discount),
                ImageName = @"wwwroot/Images/Products" + file.FileName,
                CategoryId = int.Parse(CategorySelectedId),
                Description = Description
            };

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
