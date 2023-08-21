using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain.Common.Models
{
    public class PaginationOption
    {
        public int? limit { get; set; }
        public int offset { get; set; }
    }
}
