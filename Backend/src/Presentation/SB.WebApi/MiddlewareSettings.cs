namespace SB.WebApi
{
    public class MiddlewareSettings
    {
        public bool UseTimeLoggingMiddleware { get; set; }
        public bool UseCultureMiddleware { get; set; }
        public bool UsePaginationResponseWrapperMiddleware { get; set; }
    }
}
