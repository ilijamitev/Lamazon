using System;
using System.Collections.Generic;

namespace Lamazon.Domain.Enitities
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceLineItems = new HashSet<InvoiceLineItem>();
        }

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int InvoiceStatusId { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
        public virtual InvoiceStatus InvoiceStatus { get; set; }

        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }
    }
}
