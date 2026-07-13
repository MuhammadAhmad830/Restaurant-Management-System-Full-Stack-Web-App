using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantManagmentSystemApp.Model
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Price ke liye ye lazmi hai
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string? ImageUrl { get; set; } // Baad mein pictures lagane ke liye

        // Relationship with Category
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}