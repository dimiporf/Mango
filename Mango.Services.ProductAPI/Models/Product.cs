using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models
{
    // Represents a product entity in the ProductAPI
    public class Product
    {
        [Key]
        public int ProductId { get; set; } // Unique identifier for the product

        [Required(ErrorMessage = "Product name is required")] // Specifies that the Name property is required
        public string Name { get; set; } // Name of the product

        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000")] // Specifies the valid range for the Price property
        public double Price { get; set; } // Price of the product

        public string Description { get; set; } // Description of the product

        public string CategoryName { get; set; } // Category name to which the product belongs

        public string ImageUrl { get; set; } // URL of the product image
    }
}

