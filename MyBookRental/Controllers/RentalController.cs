using MyBookRental.Models;
using MyBookRental.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace MyBookRental.Controllers
{
    [Authorize(Roles = RoleName.CanManageBooks)]
    public class RentalController : Controller
    {
        private ApplicationDbContext dbContext;

        public RentalController()
        {
            dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
        }

        [HttpGet]
        public PartialViewResult ViewRentals()
        {
            var rentals = dbContext.Rentals.Include(r => r.Book).Include(r => r.Customer).ToList();
            
           /* if(User.IsInRole(RoleName.CanManageBooks))
            {
                return PartialView("_Rental", rentals);
            }*/

            return PartialView("_Rental", rentals);
        }

        public ActionResult RentBook()
        {
            var rentalViewModel = new RentalFormViewModel
            {
                Rental = new Rental(),
                Books = dbContext.Books.Where(b => b.NumberInStock > 0).ToList(),
                Customers = dbContext.Customers.ToList()
            };

            return View(rentalViewModel);
        }

        [HttpPost]
        public PartialViewResult RentBook(Rental rental)
        {
            
            var book = dbContext.Books.Single(b => rental.BookId == b.Id);

            var currentAvailability = book.NumberInStock;


            if (currentAvailability < 1)
            {
                var updatedRentals = dbContext.Rentals.Include(r => r.Book).Include(r => r.Customer).ToList();
                return PartialView("_Rental", updatedRentals);
            }
            else
            {
                currentAvailability--;
                book.NumberInStock = currentAvailability;
                book.Availability = DetermineAvailability(currentAvailability);

                rental.DateReturned = null;
                rental.DateBorrowed = DateTime.Now;

                dbContext.Rentals.Add(rental);
                dbContext.SaveChanges();

                var updatedRentals = dbContext.Rentals.Include(r => r.Book).Include(r => r.Customer).ToList();

                return PartialView("_Rental", updatedRentals);
            }
        }

        [HttpDelete]
        public PartialViewResult ReturnBook(int id)
        {
            var rental = dbContext.Rentals.Single(r => r.Id == id);

            var book = dbContext.Books.Single(b => b.Id == rental.BookId);

            book.NumberInStock++;
            book.Availability = DetermineAvailability(book.NumberInStock);

            dbContext.Rentals.Remove(rental);

            dbContext.SaveChanges();

            var updatedRentals = dbContext.Rentals.Include(r => r.Book).Include(r => r.Customer).ToList();

            return PartialView("_Rental", updatedRentals);
        }

        private string DetermineAvailability(int numberInStock)
        {
            if(numberInStock == 0)
            {
                return "Not Available";
            }

            else
            {
                return "Available";
            }
        }   
    }
}