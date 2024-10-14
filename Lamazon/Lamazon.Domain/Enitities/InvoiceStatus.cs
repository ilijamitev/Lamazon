using System.Collections.Generic;

namespace Lamazon.Domain.Enitities
{
    public class InvoiceStatus : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }

        public InvoiceStatus()
        {
            Invoices = new HashSet<Invoice>();
        }
    }
}
