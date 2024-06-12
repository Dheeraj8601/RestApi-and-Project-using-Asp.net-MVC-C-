using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Management.Data;
using Shop_Management.Models;

namespace Shop_Management.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            IEnumerable <Product> products= await _db.Products.ToListAsync();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _db.Products.Find(id);
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var p= _db.Products.Find(id);
            if(p == null)
            {
                return NotFound();
            }
            _db.Products.Remove(p);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
