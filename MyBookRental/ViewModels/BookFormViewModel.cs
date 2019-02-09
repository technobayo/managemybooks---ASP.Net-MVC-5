using MyBookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookRental.ViewModels
{
    public class BookFormViewModel
    {
        public Book Book { get; set; }
        public List<Genre> Genres { get; set; }
    }
}