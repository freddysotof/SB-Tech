using NLog.Web;
using SB.WebApi.Middlewares;

namespace SB.WebApi.Extensions
{
    public static class MiddlewareExtensions
    {

        public static IApplicationBuilder UseHashAuthorizationValidator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HashAuthorizationMiddleware>();
        }

        /// <summary>
        /// Adds the Logging middleware, which logs the incoming request's path.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<NLogRequestPostedBodyMiddleware>(
              new NLogRequestPostedBodyMiddlewareOptions()
              );
            return builder.UseMiddleware<LoggingMiddleware>();
        }


     
        /// <summary>
        /// Adds the Global Error Handler Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
        }

     

       

        /// <summary>
        /// Adds the Pagination Response Wrapper Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePaginationResponseWrapperMiddleware(this IApplicationBuilder builder)
        {

            return builder.UseMiddleware<PaginationResponseWrapperMiddleware>();
        }

        /// <summary>
        /// Adds the Simple Response Wrapper Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseResponseWrapperMiddleware(this IApplicationBuilder builder)
        {

            return builder.UseMiddleware<ResponseWrapperMiddleware>();
        }

      


        /// <summary>
        /// Adds the Model State Validation Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseModelStateValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ModelStateValidationMiddleware>();
        }

       
    }
}
