using SB.Domain.Common.Models;
using System.Net;

namespace SB.Domain.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message, List<ResponseError>? errors = null)
            : base(message, errors, HttpStatusCode.BadRequest)
        {
        }
    }
}
