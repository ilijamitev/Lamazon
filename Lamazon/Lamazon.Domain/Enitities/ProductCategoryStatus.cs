namespace Lamazon.Domain.Enitities
{
    public class ProductCategoryStatus : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public ProductCategoryStatus()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

    }
}
