﻿using LoggerService;
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
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerManager<RequestResponseLoggingMiddleware> loggerManager)
        {
            _next = next;
            _logger = loggerManager;
        }

        public async Task Invoke(HttpContext context)
        {
           
            //First, get the incoming request
            var request = await FormatRequest(context.Request);
           


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

        private static async Task<string> FormatRequest(HttpRequest request)
        {
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
