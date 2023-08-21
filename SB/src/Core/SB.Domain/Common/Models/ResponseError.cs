namespace SB.Domain.Common.Models
{
    public class ResponseError : ErrorResult
    {
        public ResponseError(string statusError = null, string message = null, string stackTrace = null, string description = null)
        {
            base.StatusError = statusError;
            base.Message = message;
            base.StackTrace = stackTrace;
            base.Description = description;
        }

        public ResponseError(ErrorResult errorResult)
        {
            base.StatusError = errorResult.StatusError;
            base.Message = errorResult.Message;
            base.Description = errorResult.Description;
            base.StackTrace = errorResult.StackTrace;
        }
    }
}
