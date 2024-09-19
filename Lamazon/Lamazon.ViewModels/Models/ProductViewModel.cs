using Lamazon.ViewModels.Enums;
using System;

namespace Lamazon.ViewModels.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductStatusEnum ProductStatus { get; set; }
        public ProductCategoryViewModel ProductCategory { get; set; }
        public decimal Price { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsAddedToCart { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal DicountedPrice
        {
            get { return Math.Round((1 - (decimal)DiscountPercentage / 100) * Price, 2); }
        }
    }
}
