using SB.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain.Exceptions
{
    public class UnauthorizedAccessException : CustomException
    {
        public UnauthorizedAccessException(string message, List<ResponseError>? errors = null)
            : base(message, errors, HttpStatusCode.Unauthorized)
        {
        }
    }
}
