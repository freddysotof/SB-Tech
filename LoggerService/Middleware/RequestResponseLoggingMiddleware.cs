using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager<RequestResponseLoggingMiddleware> _logger;
        //private readonly IBus _busControl;
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerManager<RequestResponseLoggingMiddleware> loggerManager/*, IBus busControl*/)
        {
            _next = next;
            _logger = loggerManager;
            //_busControl = busControl;
        }

        //public async Task InvokeAsync(HttpContext context)
        //{
        //    context.Request.EnableBuffering();
        //    var requestReader = new StreamReader(context.Request.Body);

        //    var requestContent = await requestReader.ReadToEndAsync();
        //    context.Request.Body.Position = 0;


        //    using var ms = new MemoryStream();
        //    context.Response.Body = ms;
        //    await _next(context);


        //    ms.Position = 0;
        //    var responseReader = new StreamReader(ms);

        //    var responseContent = await responseReader.ReadToEndAsync();
        //    _logger.LogInformation($"Response Body: {responseContent}");

        //    ms.Position = 0;
        //    context.Response.Body.Seek(0, SeekOrigin.Begin);
        //}

        public async Task Invoke(HttpContext context)
        {
           
            //First, get the incoming request
            var request = await FormatRequest(context.Request);
           

            //context.Request.Body.Position = 0;


            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using var responseBody = new MemoryStream();
            //...and use that for the temporary response body
            context.Response.Body = responseBody;

            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);

            //Format the response from the server
            var response = await FormatResponse(context.Response);

            //TODO: Save log to chosen datastore
           await _logger.LogInformation(response.Item1, null, response.Item2).ConfigureAwait(false);


            //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            await responseBody.CopyToAsync(originalBodyStream);
        }

      

        //private static async Task<string> FormatRequest(HttpRequest request)
        //{
        //    //var body = request.Body;

        //    //This line allows us to set the reader for the request back at the beginning of its stream.
        //    request.EnableBuffering();


        //    //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
        //    var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        //    //...Then we copy the entire request stream into the new buffer.
        //    await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);
        //    //We convert the byte[] into a string using UTF8 encoding...
        //    var bodyAsText = Encoding.UTF8.GetString(buffer);

        //    //..and finally, assign the read body back to the request body, which is allowed because of EnableBuffering()
        //    //request.Body = body;

        //    request.Body.Position = 0;
        //    return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {requestContent}";
        //}

        private static async Task<string> FormatRequest(HttpRequest request)
        {
            //var path = request.Path.Value;
            //var httpMethod = request.Method;
            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            var requestReader = new StreamReader(request.Body);

            var requestContent = await requestReader.ReadToEndAsync();
            
            request.Body.Position = 0;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {requestContent}";
        }


        private static async Task<(string,string)> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return ($"Status Code: {response.StatusCode} {(HttpStatusCode)response.StatusCode}",text);
        }
    
    }
}
