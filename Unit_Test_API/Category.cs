using System.ComponentModel.DataAnnotations;

namespace Unit_Test_API
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        // Relationship: A Category can have multiple Products
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}