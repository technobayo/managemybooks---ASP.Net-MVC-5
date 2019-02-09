

using System.ComponentModel.DataAnnotations;

namespace MyBookRental.Models
{
    public class Book
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required][StringLength(1000)]
        public string Summary { get; set; }

        [Required][StringLength(255)]
        public string ImageReference { get; set; }

        [Required][Range(0,1)]
        public int NumberInStock { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public string Availability { get; set; }
    }
}