using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Category
    {
        [Key]
        public int Count { get; set; }
        [Required]
        public int Categoryy { get; set; }
    }
}
