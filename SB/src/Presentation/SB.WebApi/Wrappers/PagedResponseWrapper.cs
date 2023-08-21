﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.WebApi.Wrappers
{
    public class PagedResponseWrapper<T> : SuccessResponseWrapper<T>
        where T : class
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public PagedResponseWrapper(T data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            this.Data = data;
            this.Messages = new();
            this.IsSuccessStatusCode = true;
            this.StatusCode = System.Net.HttpStatusCode.OK;
        }
    }
}
