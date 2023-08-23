using SB.WebApi.Middlewares;
using SB.WebApi.Wrappers;
using System.Net;
using System.Text;
using LoggerService;
using SB.Domain.Common.Models;
using SB.Domain.Exceptions;

namespace SB.WebApi.Middlewares
{
    /// <summary>
    /// Globar Error Handler Middleware to Manage Exceptions.
    /// </summary>
    public class GlobalErrorHandlingMiddleware
    {
        /// <summary>
        /// Request Delegate field to invoke HTTP Context
        /// </summary>
        private readonly RequestDelegate _next;
        private readonly ILoggerManager<LoggingMiddleware> _logger;
        /// <summary>
        /// The Globar Error Handler Middleware Constructor
        /// </summary>
        /// <param name="next">The Request Delegate</param>
        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILoggerManager<LoggingMiddleware> loggerManager)
        {
            _next = next;
            _logger = loggerManager;
        }


        /// <summary>
        /// Invoke Method for the HttpContext
        /// </summary>
        /// <param name="context">The HTTP Context</param>
        /// <returns>Response</returns>
        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            //await _logger.LogInformation("Initating global error handler");
            Stream originalBodyStream = null;
            HttpResponse response = context.Response;
            HttpStatusCode status;


            //Create a new memory stream...
            await using var cachedResponseBody = new MemoryStream();

            try
            {
                //Copy a pointer to the original response body stream
                originalBodyStream = response.Body;

           


              
                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

            }
            catch (Exception ex)
            {
                //...and use that for the temporary or cached response body
                response.Body = cachedResponseBody;

                // Create a new memory stream...
                //...and use it to modify the response body
                using var modifiedResponseBody = new MemoryStream();


                ////We need to read the response stream from the beginning...
                //response.Body.Seek(0, SeekOrigin.Begin);

                ////...and copy it into a string
                //string body = await new StreamReader(response.Body).ReadToEndAsync();

                ////We need to reset the reader for the response so that the client can read it.
                //response.Body.Seek(0, SeekOrigin.Begin);

                // Invoking Customizations Method to handle Custom Formatted Response
                try
                {
                    long formattedBodyLength = await HandleExceptionAsync(context, modifiedResponseBody, ex);

                    // Set the modified Stream Content Length before copying in original stream
                    response.ContentLength = modifiedResponseBody.Length;

                    // Set the Content-Type Header of the response
                    response.ContentType = "application/json";

                    //Format the response from the server
                    await modifiedResponseBody.CopyToAsync(originalBodyStream);
                }
                catch (Exception e)
                {

                    throw;
                }
  
     

            }
        }


        private async Task<long> HandleExceptionAsync(HttpContext context, Stream modifiedResponseBody,Exception ex)
        {
            var response = context.Response;

            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            ////...and copy it into a string
            string body = await new StreamReader(response.Body).ReadToEndAsync();

            #region Customize Error Response
            var responseWrapper = new ErrorResponseWrapper()
            {
                Source = ex.TargetSite?.DeclaringType?.FullName,
                Exception = ex.Message.Trim(),
                SupportMessage = $"Provide the Error Id: {Guid.NewGuid()} to the support team for further analysis."
            };
            if (ex is not CustomException && ex.InnerException != null)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
            }
            ResponseError error;
            switch (ex)
            {
                case CustomException e:
                    responseWrapper.StatusCode = e.StatusCode;
                    if (e.Errors is not null)
                    {
                        responseWrapper.Errors = e.Errors;
                    }
                    break;
                default:
                    responseWrapper.StatusCode = HttpStatusCode.InternalServerError;
                    responseWrapper.Errors.Add(new("Internal Server Error"));
                    break;

            };
            string formattedResponse = responseWrapper.ToString();
            #endregion

            // Set the modified Stream Content Length before copying in original stream
            response.StatusCode = (int)responseWrapper.StatusCode;

            using (var writer = new StreamWriter(modifiedResponseBody, Encoding.UTF8, -1, true))
            {
                await writer.WriteAsync(formattedResponse);
                await writer.FlushAsync();
            }

            ////We need to reset the reader for the response so that the client can read it.
            modifiedResponseBody.Seek(0, SeekOrigin.Begin);

 

            //if (formattedResponse.Length > 0)
            //{
            //    // Set the Content-Type Header of the response
            //    response.ContentType = "application/json";

            //    // Set the StatusCode Header of the responseCatadev19@!

            //    response.StatusCode = (int)responseWrapper.StatusCode;

            //    // Set the Content Length Header of the response
            //    response.ContentLength = (long)formattedResponse.Length;

            //    //Format the response from the server

            //    await response.WriteAsync(formattedResponse);
            //}

            return (long)formattedResponse.Length;
        }
    }
}
