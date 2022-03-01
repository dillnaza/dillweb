using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Book
    {
        [Key]
        public int Count { get; set; }
        [Required]
        public string Name { get; set; }
        public string Author { get; set; }
        public int Category { get; set; }
    }
}
