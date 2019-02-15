using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookRental.Models;
using MyBookRental.ViewModels;

namespace MyBookRental.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext dbContext;

        public BookController()
        {
            dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var books = dbContext.Books.Include(b => b.Genre).ToList();
            return View(books);
        }

        public ActionResult IndexFromApi()
        {
            return View();
        }

        public ActionResult Index_Admin()
        {
            var books = dbContext.Books.Include(b => b.Genre).ToList();
            return View(books);
        }

        [HttpGet]
        public ActionResult AddNewBook()
        {
            var bookViewModel = new BookFormViewModel
            {
                Book = new Book(),
                Genres = dbContext.Genres.ToList()
            };

            return View(bookViewModel);
        }

        [HttpPost]
        public ActionResult AddNewBook(Book book)
        {
            if(!ModelState.IsValid)
            {
                var bookViewModel = new BookFormViewModel
                {
                    Book = new Book(),
                    Genres = dbContext.Genres.ToList()
                };

                return View(bookViewModel);
            }

            dbContext.Books.Add(book);
            dbContext.SaveChanges();

            return RedirectToAction("Index","Book");
        }

        [AllowAnonymous]
        public ActionResult ViewBookDetails(int id)
        {
            var book = dbContext.Books.Include(b => b.Genre).SingleOrDefault(b => b.Id == id);
            if(book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        public ActionResult ViewBookDetails_Admin(int id)
        {
            var book = dbContext.Books.Include(b => b.Genre).SingleOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        public ActionResult EditBook(int id)
        {
            var bookInDb = dbContext.Books.Single(b => b.Id == id);

            var bookViewModel = new BookFormViewModel
            {
                Book = bookInDb,
                Genres = dbContext.Genres.ToList()
            };

            return View(bookViewModel);
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            var bookInDb = dbContext.Books.Single(b => b.Id == book.Id);

            if (!ModelState.IsValid)
            {
                var bookViewModel = new BookFormViewModel
                {
                    Book = bookInDb,
                    Genres = dbContext.Genres.ToList()
                };

                return View(bookViewModel);
            }

            bookInDb.Title = book.Title;
            bookInDb.Summary = book.Summary;
            bookInDb.Author = book.Author;
            bookInDb.GenreId = book.GenreId;
            bookInDb.NumberInStock = book.NumberInStock;
            bookInDb.Availability = book.Availability;
            bookInDb.ImageReference = book.ImageReference;

            dbContext.SaveChanges();

            return RedirectToAction("Index", "Book");
        }

        [HttpDelete]
        public ActionResult RemoveBook(int id)
        {
            var book = dbContext.Books.Single(b => b.Id == id);

            dbContext.Books.Remove(book);
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Book");
        }

        /* public ActionResult DeleteBook(int id)
         {
             var book = dbContext.Books.Single(b => b.Id == id);


             return View(book);
         }

         [HttpDelete]
         public ActionResult DeleteBook(Book book)
         {
             var bookInDb = dbContext.Books.Single(b => b.Id == book.Id);

             dbContext.Books.Remove(bookInDb);
             dbContext.SaveChanges();

             return RedirectToAction("Index", "Book");
         }*/

    }
}