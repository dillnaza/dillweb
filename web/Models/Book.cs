using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Book
    {
        [Key]
        public int Count { get; set; }

        [Required]
        [DisplayName("Называниe книги:")]
        public string Name { get; set; }

        [DisplayName("Автор книги:")]
        public string Author { get; set; }

        [DisplayName("Категория:")]
        [Range(1, 5, ErrorMessage = "категория может быть только от 1 до 5")]
        public int Category { get; set; }
    }
}
