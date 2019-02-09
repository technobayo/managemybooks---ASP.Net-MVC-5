using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookRental.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required][Display(Name = "Date Borrowed")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateBorrowed { get; set; }

        [Display(Name = "Date Returned")]
        public DateTime? DateReturned { get; set; }

        public Book Book { get; set; }

        public Customer Customer { get; set; }

        [Display(Name = "Available Books")]
        public int BookId { get; set; }

        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }
    }
}