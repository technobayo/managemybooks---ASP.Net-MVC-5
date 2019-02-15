using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyBookRental.Models;
using MyBookRental.ViewModels;

namespace MyBookRental.Controllers.APIs
{
    public class CustomerController : ApiController
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

        [HttpGet]
        public IHttpActionResult Get(string firstname = null)
        {
            var customers = dbContext.Customers.ToList();

            if(!string.IsNullOrWhiteSpace(firstname))
            {
                var specificCustomer = dbContext.Customers.Where(c => c.FirstName.Contains(firstname)).ToList();
                return Ok(specificCustomer);
            }

            return Ok(customers);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var customer = dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IHttpActionResult Post(Customer customer)
        {
            if(ModelState.IsValid)
            {
                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();
            }

            return Created(new Uri(Request.RequestUri + customer.Id.ToString()), customer);
        }

        [HttpPut]
        public IHttpActionResult Put(Customer customer)
        {
            var customerInDb = dbContext.Customers.Single(c => c.Id == customer.Id);
            if(ModelState.IsValid)
            {
                customerInDb.FirstName = customer.FirstName;
                customerInDb.LastName = customer.LastName;
                customerInDb.Phone = customer.Phone;
                customerInDb.Email = customer.Email;

                dbContext.SaveChanges();
            }

            return Ok(customer);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var customerInDb = dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDb == null)
            {
                return NotFound();
            }

            dbContext.Customers.Remove(customerInDb);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
