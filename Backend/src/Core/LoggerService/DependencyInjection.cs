using LoggerService.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace LoggerService
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Agrega el servicio ILoggerManager como Singleton
        /// </summary>
        /// 
        public static void ConfigureLoggerService(this IServiceCollection services, IConfiguration configuration, ILoggingBuilder loggingBuilder/*, IWebHostEnvironment env*/)
        {
            var configFileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "nlog.config").ToString();
            if (File.Exists(configFileLocation))
            {
                NLog.LogManager.LoadConfiguration(configFileLocation);

            }

            services.AddSingleton(typeof(ILoggerManager<>), typeof(LoggerManager<>));

            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(LogLevel.Trace);

            loggingBuilder.AddConsole(); //Adds a console logger named 'Console' to the factory.
            loggingBuilder.AddDebug(); //Adds a debug logger named 'Debug' to the factory.
            loggingBuilder.AddEventSourceLogger(); //Adds an event logger named 'EventSource' to the fact
            loggingBuilder.AddNLog(configFileLocation);


           
        }

        public static void ConfigureRequestLoggingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            // needed for  ${aspnet-request-posted-body} with an API Controller.
            // The options default to only logging a maximum of 30KB, since above that the ASP.NET Core framework
            // uses a temporary file on disk instead of a memory buffer.  
            // Also, only content types starting with ‘text/‘, or those ending with ‘xml’, ‘html’, ‘json’, or content types
            // that have the word ‘charset’ are logged, since we desire to log strings and not binary content
            // Those can be overridden in the options if necessary.  But typically the default options should be adequate.
            applicationBuilder.UseMiddleware<NLogRequestPostedBodyMiddleware>(
                new NLogRequestPostedBodyMiddlewareOptions()
                );
            //Add our new middleware to the pipeline
            applicationBuilder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }

    }
}
