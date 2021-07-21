using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Common;
using OnlineArasan.Models;

namespace Web.Controllers
{

    public class OrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            List<OrderDetailsViewModel> orderDetailsViewModel = GetOrderDetailViewModels();

            return View(orderDetailsViewModel);
        }

        private List<OrderDetailsViewModel> GetOrderDetailViewModels()
        {
            var orderDetails = _context.OrderDetails.ToListAsync().Result;
            List<OrderDetailsViewModel> orderDetailsVM = new List<OrderDetailsViewModel>();
            foreach (var item in orderDetails)
            {
                OrderDetailsViewModel vm = new OrderDetailsViewModel();
                vm.OrderId = item.OrderId;
                var CustomerId = GetCustomerId(item.OrderId);
              
                if (CustomerId == 0)
                    continue;
                var foundCustomer = GetCustomerNameAndDetails(CustomerId);
                var foundProduct = GetProductDetails(item.ProductId);
                vm.CustomerDetail = foundCustomer;
                vm.ProductDetails = foundProduct;
                vm.CategoryDetails = GetCategoryDetails(foundProduct.CategoryId);
                vm.SingleOrderDetail = GetOrder(item.OrderId);
                orderDetailsVM.Add(vm);
            }
            return orderDetailsVM;
        }

        private Order GetOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            return order;
        }

        private Customer GetCustomerNameAndDetails(int customerId)
        {
            var foundCustomer = _context.Customer.Find(customerId);
            return foundCustomer;
        }

        private Product GetProductDetails(int productId)
        {
            var product = _context.Products.Find(productId);
            return product;
        }
        private Category GetCategoryDetails(int categoryId)
        {
            var cate = _context.Categories.Find(categoryId);
            return cate;
        }



        private string GetCustomerName(string customerId)
        {
            var foundCustomer = _context.Customer.Find(customerId);
            if (foundCustomer != null)
            {
                return foundCustomer.CustomerName;
            }
            return string.Empty;
        }

        private int GetCustomerId(int orderId)
        {
            var foundOrder = _context.Orders.Find(orderId);
            if (foundOrder != null)
            {
                return foundOrder.CustomerId;
            }
            return 0;

        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,ProductId,Price,Quantity")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return NotFound();
            }
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,ProductId,Price,Quantity")] OrderDetails orderDetails)
        {
            if (id != orderDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailsExists(orderDetails.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetails);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetails = await _context.OrderDetails.FindAsync(id);
            _context.OrderDetails.Remove(orderDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailsExists(int id)
        {
            return _context.OrderDetails.Any(e => e.Id == id);
        }
    }
}
