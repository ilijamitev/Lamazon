using System.Collections.Generic;

namespace Lamazon.Domain.Enitities
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCategoryStatusId { get; set; }

        public virtual ProductCategoryStatus ProductCategoryStatus { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
