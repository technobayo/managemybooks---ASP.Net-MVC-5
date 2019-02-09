using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookRental.Models;

namespace MyBookRental.ViewModels
{
    public class RentalFormViewModel
    {
        public Rental Rental { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}