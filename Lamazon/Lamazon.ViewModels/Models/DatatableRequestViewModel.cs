using System.Collections.Generic;

namespace Lamazon.ViewModels.Models
{
    public class DatatableSearchViewModel
    {
        /// <summary>
        /// Global search value. To be applied to all columns which have searchable as true
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// true if the global filter should be treated as a regular expression for advanced 
        /// searching, false otherwise. Note that normally server-side processing scripts 
        /// will not perform regular expression searching for performance reasons on large 
        /// data sets, but it is technically possible and at the discretion of your script
        /// </summary>
        public bool regex { get; set; }
    }

    public class DatatableColumnViewModel
    {
        /// <summary>
        /// Column's data source
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// Column's name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Flag to indicate if this column is searchable (true) or not (false)
        /// </summary>
        public bool searchable { get; set; }

        /// <summary>
        /// Flag to indicate if this column is orderable (true) or not (false)
        /// </summary>
        public bool orderable { get; set; }

        /// <summary>
        /// Search to apply to this specific column.
        /// </summary>
        public DatatableSearchViewModel search { get; set; }
    }

    public class DatatableOrderViewModel
    {
        /// <summary>
        /// Column to which ordering should be applied. This is an index reference to the 
        /// columns array of information that is also submitted to the server
        /// </summary>
        public int? column { get; set; }

        /// <summary>
        /// Ordering direction for this column. It will be asc or desc to indicate ascending
        /// ordering or descending ordering, respectively
        /// </summary>
        public string dir { get; set; }
    }

    public class DatatableRequestViewModel
    {
        /// <summary>
        /// Draw counter. This is used by DataTables to ensure that the Ajax returns from 
        /// server-side processing requests are drawn in sequence by DataTables 
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// Paging first record indicator. This is the start point in the current data set 
        /// (0 index based - i.e. 0 is the first record)
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// Number of records that the table can display in the current draw. It is expected
        /// that the number of records returned will be equal to this number, unless the 
        /// server has fewer records to return. Note that this can be -1 to indicate that 
        /// all records should be returned (although that negates any benefits of 
        /// server-side processing!)
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// Global Search for the table
        /// </summary>
        public DatatableSearchViewModel search { get; set; }

        /// <summary>
        /// Collection of all columns in the table
        /// </summary>
        public List<DatatableColumnViewModel> columns { get; set; }

        /// <summary>
        /// Collection of all column indexes and their sort directions
        /// </summary>
        public List<DatatableOrderViewModel> order { get; set; }

        public string sortColumn
        {
            get
            {
                if (order?.Count > 0 && columns?.Count > 0 && columns.Count > order[0].column)
                {
                    return columns[order[0].column.Value].name;
                }
                return null;
            }
        }

        public bool isAscending
        {
            get
            {
                if (order?.Count > 0 && columns?.Count > 0 && columns.Count > order[0].column)
                {
                    return order[0].dir.ToUpper() != "DESC";
                }
                return true;
            }
        }
    }
}
