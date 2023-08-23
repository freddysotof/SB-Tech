using Newtonsoft.Json;

namespace SB.Domain.Common.Models
{
    public class ErrorResult
    {
        public string StatusError { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }

        public string StackTrace { get; set; }

        public ErrorResult(string statusError = null, string message = null, string stackTrace = null, string description = null)
        {
            StatusError = statusError;
            Message = message;
            StackTrace = stackTrace;
            Description = description;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
