using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unit_Test_API.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; private set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 1_000_000)]
        public decimal Price { get; private set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; private set; }

        [StringLength(50)]
        public string? Color { get; private set; }

        // Foreign Key
        [Required]
        public int CategoryId { get; private set; }

        // Navigation Property
        public Category? Category { get; private set; }

        protected Product() { }

        public Product(string name, decimal price, int stock, int categoryId, string? color = null)
        {
            SetName(name);
            SetPrice(price);
            SetStock(stock);

            CategoryId = categoryId;
            Color = color;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");

            Name = name.Trim();
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            Price = price;
        }

        public void SetStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.");

            Stock = stock;
        }

        public void IncreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Increase amount must be positive.");

            Stock += amount;
        }

        public void DecreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Decrease amount must be positive.");

            if (Stock - amount < 0)
                throw new InvalidOperationException("Insufficient stock.");

            Stock -= amount;
        }
    }
}