using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using LoggerService.Middleware;
using Microsoft.AspNetCore.Builder;
using NLog.Web;

namespace LoggerService.Extensions
{
    public static class LoggerExtensions
    {
   


        public static IDisposable BeginScopeWith(this ILogger logger, params (string key, object value)[] keys)
        {
            return logger.BeginScope(keys.ToDictionary(x => x.key, x => x.value));
        }

   
    }
}
