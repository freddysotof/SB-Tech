using SB.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace SB.WebApi.Middlewares
{
    /// <summary>
    /// Response Wrapper Middleware to Request Delegate and handles Response Customizations.
    /// </summary>
    public class ResponseWrapperMiddleware
    {
        /// <summary>
        /// Request Delegate field to invoke HTTP Context
        /// </summary>
        private readonly RequestDelegate _next;
        private IResponseWrapperService<object> _wrapperService;
        /// <summary>
        /// The Response Wrapper Middleware Constructor
        /// </summary>
        /// <param name="next">The Request Delegate</param>
        public ResponseWrapperMiddleware(RequestDelegate next) => _next = next;

        /// <summary>
        /// Invoke Method for the HttpContext
        /// </summary>
        /// <param name="context">The HTTP Context</param>
        /// <param name="wrapperService">The Wrapper Service to return custom response (paginated response or simple response)</param>
        /// <returns>Response</returns>
        public async Task Invoke(HttpContext context, IResponseWrapperService<object> wrapperService)
        {
            _wrapperService = wrapperService;


            context.Request.EnableBuffering();


            Stream originalBodyStream = null;
            HttpResponse response = context.Response;
            HttpStatusCode status;

            //Copy a pointer to the original response body stream
            originalBodyStream = response.Body;

            //Create a new memory stream...
            await using var cachedResponseBody = new MemoryStream();

            //...and use that for the temporary or cached response body
            response.Body = cachedResponseBody;

            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);

            // Create a new memory stream...
            //...and use it to modify the response body
            using var modifiedResponseBody = new MemoryStream();


            // Invoking Customizations Method to handle Custom Formatted Response
            long formattedBodyLength = await HandleResponseAsync(context, modifiedResponseBody);

            // Set the modified Stream Content Length before copying in original stream
            response.ContentLength = modifiedResponseBody.Length;

            // Set the Content-Type Header of the response
            response.ContentType = "application/json";


            //Format the response from the server
            await modifiedResponseBody.CopyToAsync(originalBodyStream);

        }

        private async Task<long> HandleResponseAsync(HttpContext context, Stream modifiedResponseBody)
        {
            var response = context.Response;

            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            ////...and copy it into a string
            string body = await new StreamReader(response.Body).ReadToEndAsync();

            // Verify if response body has any value
            if (body.Length > 0)
            {
                // Invoking Customizations Method to handle Custom Formatted Response
                var responseHandler = await _wrapperService.WrapResponseAsync(body);

                string formattedBody = responseHandler.ToString();

                using (var writer = new StreamWriter(modifiedResponseBody, Encoding.UTF8, -1, true))
                {
                    await writer.WriteAsync(formattedBody);
                    await writer.FlushAsync();
                }

                ////We need to reset the reader for the response so that the client can read it.
                modifiedResponseBody.Seek(0, SeekOrigin.Begin);
                return (long)formattedBody.Length;
            }
            return (long)body.Length;
        }
    }
}

