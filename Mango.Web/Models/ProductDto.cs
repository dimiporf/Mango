﻿using System.Security.Cryptography.X509Certificates;

namespace Mango.Web.Models
{
    // Represents a product data transfer object (DTO) used for web communication
    public class ProductDto
    {
        public int ProductId { get; set; } // Unique identifier for the product

        public string Name { get; set; } // Name of the product

        public double Price { get; set; } // Price of the product

        public string Description { get; set; } // Description of the product

        public string CategoryName { get; set; } // Category name to which the product belongs

        public string ImageUrl { get; set; } // URL of the product image
    }
}

