using System.Collections.Generic;

namespace Lamazon.Domain.Enitities
{
    public class User
    {
        public User()
        {
            Invoices = new HashSet<Invoice>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string RoleKey { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
