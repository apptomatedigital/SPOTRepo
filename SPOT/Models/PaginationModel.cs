using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class PaginationModel
    {

        public int totalCount { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public string previousPage { get; set; }
        public string nextPage { get; set; }

    }
}