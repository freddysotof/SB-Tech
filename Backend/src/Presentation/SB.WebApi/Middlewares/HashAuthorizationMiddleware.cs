using Microsoft.Extensions.Primitives;
using SB.Domain.Common.Models;
using SB.WebApi.Services;
using System.Linq;
using System.Runtime.CompilerServices;
using UnauthorizedAccessException = SB.Domain.Exceptions.UnauthorizedAccessException;
namespace SB.WebApi.Middlewares
{
    /// <summary>
    /// Model State Validation Middleware to Request Delegate and handles Model Binding Exceptions.
    /// </summary>
    public class HashAuthorizationMiddleware
    {

        /// <summary>
        /// Request Delegate field to invoke HTTP Context
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Application Configuration
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Get Hash Validator Service
        /// </summary>
        //private readonly IEncryptionValidator _encryptionValidator;

        /// <summary>
        /// The Model State Validation Middleware Constructor
        /// </summary>
        /// <param name="next">The Request Delegate</param>
        public HashAuthorizationMiddleware(RequestDelegate next) => _next = next;

        /// <summary>
        /// Invoke Method for the HttpContext
        /// </summary>
        /// <param name="context">The HTTP Context</param>
        /// <returns>Exception</returns>
        /// <returns>Response</returns>
        public async Task Invoke(HttpContext context, IEncryptionValidator encryptionValidator, IConfiguration configuration)
        {

            //First, get the incoming request
            var request = await FormatRequest(context.Request);
            var authSignature = request.Headers.FirstOrDefault(x => x.Key.ToLower() == "x-auth-signature").Value ?? throw new UnauthorizedAccessException($"Missing signature header");
            var isRequestValid = encryptionValidator.VerifyAuthenticity(authSignature, configuration["MiddlewareSettings:EncryptionHashKey"], request.Body);
            if (!isRequestValid)
                throw new UnauthorizedAccessException($"Signature hash is not valid"); ;
            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);






        }
        private async Task<RequestContent> FormatRequest(HttpRequest request)
        {
            RequestContent requestContent = new();

            requestContent.Path = request.Path.Value;
            requestContent.Method = request.Method;
            requestContent.UserName = request.HttpContext.User.Identity.Name;
            requestContent.Headers = request.Headers.ToDictionary(s => s.Key.ToString(), s => s.Value.ToString());
            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            var requestReader = new StreamReader(request.Body);

            requestContent.Body = await requestReader.ReadToEndAsync();


            request.Body.Position = 0;

            requestContent.Content = $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {requestContent.Body}";
            return requestContent;
        }
    }
}
