using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Shop_Management.Data;
using Shop_Management.Models;
using System.Linq;

namespace Shop_Management.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrdersController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Order> orders = _db.Orders
                //.Include(o => o.CustomerId)
                .Include(o => o.OrderItems)
                //.ThenInclude(oi => oi.Product)
                .ToList();

            var customers = _db.Customers.ToDictionary(c => c.Id, c => c.FullName); 

            ViewBag.Customers = customers;
            return View(orders);
        }


        public IActionResult Details(int id)
        {
            var order = _db.Orders
                //.Include(o => o.Customer)
                .Include(o => o.OrderItems)
               // .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);

            var customers = _db.Customers.ToDictionary(c => c.Id, c => c.FullName);
            ViewBag.Customers= customers;
            var products = _db.Products.ToDictionary(p => p.Id, p => p.Name);
            ViewBag.Products = products;

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_db.Customers, "Id", "FullName");
            ViewBag.Products = new SelectList(_db.Products, "Id", "Name");
            var order = new Order
            {
                OrderDate = DateTime.Now
            };
            return View(order);
        }



        [HttpPost]
        public IActionResult Create(Order order)
        {
  
            foreach (var item in order.OrderItems)
            {
                item.OrderId = order.Id;
                item.ProductId = item.ProductId;
                item.Quantity = item.Quantity;
                item.UnitPrice = item.UnitPrice;
                _db.OrderItems.Add(item);
            }

            if (ModelState.IsValid)
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            ViewBag.Customers = new SelectList(_db.Customers, "Id", "FullName");
            ViewBag.Products = new SelectList(_db.Products, "Id", "Name");
            return View(order);
        }
    }
}
