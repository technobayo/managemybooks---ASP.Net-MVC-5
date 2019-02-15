using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MyBookRental.Models;
using System.Data.Entity;

namespace MyBookRental.Controllers.APIs
{
    public class BookController : ApiController
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public IEnumerable<Book> GetBooks()
        {
            return dbContext.Books.Include(b => b.Genre).ToList();
        }

        public IHttpActionResult GetBook(int id)
        {
            Book book = dbContext.Books.SingleOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.Books.Add(book);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }
        
        public IHttpActionResult PutBook(int id,Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Book bookInDb = dbContext.Books.SingleOrDefault(b => b.Id == id);

            if (bookInDb == null)
            {
                return BadRequest();
            }

            bookInDb.Title = book.Title;
            bookInDb.Summary = book.Summary;
            bookInDb.Author = book.Author;
            bookInDb.GenreId = book.GenreId;
            bookInDb.NumberInStock = book.NumberInStock;
            bookInDb.Availability = book.Availability;
            bookInDb.ImageReference = book.ImageReference;

            dbContext.SaveChanges();

            return Ok();
        }

        public IHttpActionResult DeleteBook(int id)
        {
            Book book = dbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            dbContext.Books.Remove(book);
            dbContext.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}