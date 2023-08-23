using SB.Domain.Common.Models;
using System.Net;

namespace SB.Domain.Exceptions
{
    public class CustomException : Exception
    {
        public List<ResponseError>? Errors { get; }

        public HttpStatusCode StatusCode { get; }

        public CustomException(string message, List<ResponseError>? errors = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            Errors = errors;
            StatusCode = statusCode;
        }
    }
}
