using System.Collections.Generic;

namespace Lamazon.ViewModels.Models
{
    public class PagedResultViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }

        public object ToTableData()
        {
            return new
            {
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalDisplayRecords,
                aaData = Items
            };
        }
    }
}
