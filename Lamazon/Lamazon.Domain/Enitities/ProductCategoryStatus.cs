using System.Collections.Generic;

namespace Lamazon.Domain.Enitities
{
    public class ProductCategoryStatus
    {
        public ProductCategoryStatus()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
