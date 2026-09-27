using System;
using System.Collections.Generic;
using System.Text;

namespace Dima.Core.Requests
{
    public abstract class PagedRequest : Request
    {
        public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
    }
}
