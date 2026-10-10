using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex {  get; set; }
        public int PageSize {  get; set; }
    }
}
