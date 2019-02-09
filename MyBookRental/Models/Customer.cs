using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookRental.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required][StringLength(255)][Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Borrowed by")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}