using System.Collections.Generic;

namespace Lamazon.Domain.Enitities
{
    public class OrderStatus : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }
    }
}
