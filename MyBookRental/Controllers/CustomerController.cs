using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookRental.Models;

namespace MyBookRental.Controllers
{
    [Authorize(Roles = RoleName.CanManageBooks)]
    public class CustomerController : Controller
    {

        private ApplicationDbContext dbContext;

        public CustomerController()
        {
            dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
        }

        // GET: Customer
        public ActionResult Index()
        {
            var customers = dbContext.Customers.ToList();
            return View(customers);
        }

        public ActionResult AddNewCustomer()
        {
            var customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public ActionResult AddNewCustomer(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return View(customer);
            }

            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
            return RedirectToAction("Index","Customer");
        }

        public ActionResult ViewCustomerDetails(int id)
        {
            var customer = dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        public ActionResult EditCustomer(int id)
        {
            var customer = dbContext.Customers.Single(c => c.Id == id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer customer)
        {
            var customerInDb = dbContext.Customers.Single(c => c.Id == customer.Id);

            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            customerInDb.FirstName = customer.FirstName;
            customerInDb.LastName = customer.LastName;
            customerInDb.Phone = customer.Phone;
            customerInDb.Email = customer.Email;

            dbContext.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult DeleteCustomer(int id)
        {
            var customer = dbContext.Customers.Single(c => c.Id == id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(Customer customer)
        {
            var customerInDb = dbContext.Customers.Single(c => c.Id == customer.Id);

            dbContext.Customers.Remove(customerInDb);
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }
    }
}