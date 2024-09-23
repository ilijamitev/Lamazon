using Lamazon.Domain.Enitities;
using Microsoft.EntityFrameworkCore;

namespace Lamazon.DataAccess.Extensions
{
    public static class DataSeedExtensions
    {
        public static ModelBuilder SeedProductCategoryStatus(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategoryStatus>().HasData(
                new ProductCategoryStatus { Id = 1, Name = "Active" },
                new ProductCategoryStatus { Id = 255, Name = "Deleted" }
            );
            return modelBuilder;
        }

        public static ModelBuilder SeedProductStatus(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductStatus>().HasData(
                new ProductStatus { Id = 1, Name = "Active" },
                new ProductStatus { Id = 255, Name = "Deleted" }
            );
            return modelBuilder;
        }

        public static ModelBuilder SeedProductCategory(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Id = 1, Name = "Food", ProductCategoryStatusId = 1 },
                new ProductCategory { Id = 2, Name = "Drinks", ProductCategoryStatusId = 1 },
                new ProductCategory { Id = 3, Name = "Books", ProductCategoryStatusId = 1 },
                new ProductCategory { Id = 4, Name = "Software", ProductCategoryStatusId = 1 },
                new ProductCategory { Id = 5, Name = "Computers", ProductCategoryStatusId = 1 }
            );
            return modelBuilder;
        }
    }
}
