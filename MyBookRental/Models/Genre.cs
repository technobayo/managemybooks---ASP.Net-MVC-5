using System.ComponentModel.DataAnnotations;

namespace MyBookRental.Models
{
    public class Genre
    {
        public byte Id { get; set; }

        [Required][Display(Name = "Genre")]
        public string Name { get; set; }
    }
}