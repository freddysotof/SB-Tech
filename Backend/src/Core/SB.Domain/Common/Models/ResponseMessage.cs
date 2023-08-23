namespace SB.Domain.Common.Models
{
    public class ResponseMessage
    {
        public string StatusMessage { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }

        public ResponseMessage(string statusMessage = null, string message = null, string description = null)
        {
            StatusMessage = statusMessage;
            Message = message;
            Description = description;
        }
    }
}
