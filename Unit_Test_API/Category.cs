using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unit_Test_API.Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; private set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; private set; }

        // Navigation Property
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        // EF Constructor
        protected Category() { }

        public Category(string name, string? description = null)
        {
            SetName(name);
            Description = description;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty.");

            Name = name.Trim();
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
        }
    }
}