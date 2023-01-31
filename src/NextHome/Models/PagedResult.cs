using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextHome.Models
{
    public class PagedResult<T>
    {
        public long TotalPages { get; set; }
        public long TotalItems { get; set; }
        public long ItemsPerPage { get; set; }
        public long CurrentPage { get; set; }
        public List<T> Items { get; set; }
    }
}