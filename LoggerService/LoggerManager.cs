using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService
{
    public class LoggerManager<T> : ILoggerManager<T> where T : class

    {
        private readonly ILogger _logger;
        private string _category { get; set; }
        public LoggerManager(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public string GetCurrentMethodName(int frame = 1)
        {
            var currentMethod = new StackTrace().GetFrame(frame).GetMethod();
            return $"{currentMethod.DeclaringType.FullName}.{currentMethod.Name}";
        }
        public Task LogInformation(string message,string reference,string json)
        {

            //note: render userId via ${mdlc:userid}
            using (_logger.BeginScope(new[] {
                new KeyValuePair<string, object>("action",GetCurrentMethodName(2)),
                new KeyValuePair<string, object>(nameof(reference), reference),
                new KeyValuePair<string, object>(nameof(json),json)
                }))
            {
                _logger.LogInformation(message);
            }
            //using _loggerFactory.BeginScopeWith((nameof(reference), reference), (nameof(json), json)){
            //    _logger.LogInformationrmation(message);
            return Task.CompletedTask;
        }
        //public Task LogInformation(string message, string reference, string json)
        //{

        //    //note: render userId via ${mdlc:userid}
        //    using (_logger.BeginScope(new[] {
        //        new KeyValuePair<string, object>(nameof(reference), reference),
        //        new KeyValuePair<string, object>(nameof(json),json)
        //        }))
        //    {
        //        _logger.LogInformation(message);
        //    }
        //    //using _loggerFactory.BeginScopeWith((nameof(reference), reference), (nameof(json), json)){
        //    //    _logger.LogInformationrmation(message);
        //    return Task.CompletedTask;
      //  return Task.CompletedTask;
        public Task LogWarning( string message, string reference, string json)
        {
            //note: render userId via ${mdlc:userid}
            using (_logger.BeginScope(new[] {
                new KeyValuePair<string, object>("action",GetCurrentMethodName(2)),
                new KeyValuePair<string, object>(nameof(reference), reference),
                new KeyValuePair<string, object>(nameof(json),json)
                }))
            {
                _logger.LogWarning(message);
            }

            //using _loggerFactory.BeginScopeWith((nameof(reference), reference), (nameof(json), json)){
            //    _logger.LogInformationrmation(message);
            return Task.CompletedTask;
        }
        public Task LogWarning(Exception e,string message, string reference, string json)
        {
            //note: render userId via ${mdlc:userid}
            using (_logger.BeginScope(new[] {
                new KeyValuePair<string, object>("action",GetCurrentMethodName(2)),
                new KeyValuePair<string, object>(nameof(reference), reference),
                new KeyValuePair<string, object>(nameof(json),json)
                }))
            {
                _logger.LogWarning(e,message);
            }

            //using _loggerFactory.BeginScopeWith((nameof(reference), reference), (nameof(json), json)){
            //    _logger.LogInformationrmation(message);
            return Task.CompletedTask;
        }
        public Task LogError( string message, string reference, string json)
        {
            //note: render userId via ${mdlc:userid}
            using (_logger.BeginScope(new[] {
                new KeyValuePair<string, object>("action",GetCurrentMethodName(2)),
                new KeyValuePair<string, object>(nameof(reference), reference),
                new KeyValuePair<string, object>(nameof(json),json)
                }))
            {
                _logger.LogError(message);
            }

            //using _loggerFactory.BeginScopeWith((nameof(reference), reference), (nameof(json), json)){
            //    _logger.LogInformationrmation(message);
            return Task.CompletedTask;
        }
        public Task LogError(Exception e,string message)
        {
            _logger.LogError(e, message);
            return Task.CompletedTask;
        }
        public Task LogError(Exception e,string message, string reference, string json)
        {
            //note: render userId via ${mdlc:userid}
            using (_logger.BeginScope(new[] {
                new KeyValuePair<string, object>("action",GetCurrentMethodName(2)),
                new KeyValuePair<string, object>(nameof(reference), reference),
                new KeyValuePair<string, object>(nameof(json),json)
                }))
            {
                _logger.LogError(e,message);
            }

            //using _loggerFactory.BeginScopeWith((nameof(reference), reference), (nameof(json), json)){
            //    _logger.LogInformationrmation(message);
            return Task.CompletedTask;
        }
        public Task LogError(Exception e, string message, string reference)
        {
            //note: render userId via ${mdlc:userid}
            using (_logger.BeginScope(new[] {
                new KeyValuePair<string, object>("action",GetCurrentMethodName(2)),
                new KeyValuePair<string, object>(nameof(reference), reference),
                }))
            {
                _logger.LogError(e, message);
            }

            //using _loggerFactory.BeginScopeWith((nameof(reference), reference), (nameof(json), json)){
            //    _logger.LogInformationrmation(message);
            return Task.CompletedTask;
        }
        public Task LogCritical( string message, string reference, string json)
        {
            //note: render userId via ${mdlc:userid}
            using (_logger.BeginScope(new[] {
                new KeyValuePair<string, object>("action",GetCurrentMethodName(2)),
                new KeyValuePair<string, object>(nameof(reference), reference),
                new KeyValuePair<string, object>(nameof(json),json)
                }))
            {
                _logger.LogCritical(message);
            }

            //using _loggerFactory.BeginScopeWith((nameof(reference), reference), (nameof(json), json)){
            //    _logger.LogInformationrmation(message);
            return Task.CompletedTask;
        }
        public Task LogCritical(Exception e,string message, string reference, string json)
        {
            //note: render userId via ${mdlc:userid}
            using (_logger.BeginScope(new[] {
                new KeyValuePair<string, object>("action",GetCurrentMethodName(2)),
                new KeyValuePair<string, object>(nameof(reference), reference),
                new KeyValuePair<string, object>(nameof(json),json)
                }))
            {
                _logger.LogCritical(e,message);
            }

            //using _loggerFactory.BeginScopeWith((nameof(reference), reference), (nameof(json), json)){
            //    _logger.LogInformationrmation(message);
            return Task.CompletedTask;
        }
    }
}
