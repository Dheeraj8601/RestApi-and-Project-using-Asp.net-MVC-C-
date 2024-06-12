using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_Management.Data;
using Shop_Management.Models;
using System.Linq;

namespace Shop_Management.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomersController(ApplicationDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Customer> customers = _db.Customers.ToList();
            return View(customers);
        }

        public IActionResult Details(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                //hashing
                var passwordHasher = new PasswordHasher<Customer>();
                customer.Password = passwordHasher.HashPassword(customer, customer.Password);

               
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var customers = _db.Customers.Find(id);
            return View(customers);
        }

       

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var existingCustomer = _db.Customers.Find(customer.Id);
                if (existingCustomer != null)
                {
                    existingCustomer.FirstName = customer.FirstName;
                    existingCustomer.LastName = customer.LastName;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.Password = existingCustomer.Password;

                    _db.Customers.Update(existingCustomer);
                    _db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            return View(customer);
        }
      

        public IActionResult Delete(int id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var customers = _db.Customers.Find(id);
            return View(customers);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var p = _db.Customers.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            _db.Customers.Remove(p);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        // Change Password Methods
        [HttpGet]
        public IActionResult VerifyPassword(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult VerifyPassword(int id, string currentPassword)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            var passwordHasher = new PasswordHasher<Customer>();
            var verificationResult = passwordHasher.VerifyHashedPassword(customer, customer.Password, currentPassword);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Incorrect current password.");
                return View(customer);
            }

            return RedirectToAction(nameof(ChangePassword), new { id = customer.Id });
        }

        [HttpGet]
        public IActionResult ChangePassword(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult ChangePassword(int id, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                //ModelState.AddModelError(string.Empty, "New password and confirmation password do not match.");
                return View();
            }

            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            var passwordHasher = new PasswordHasher<Customer>();
            customer.Password = passwordHasher.HashPassword(customer, newPassword);

            _db.Customers.Update(customer);
            _db.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = customer.Id });
        }
    }
}
