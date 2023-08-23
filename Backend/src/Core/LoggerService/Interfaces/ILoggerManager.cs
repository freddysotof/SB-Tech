using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService
{
    public interface ILoggerManager<T> where T : class
    {
        //void LogInformation(string action=null,string message=null, string reference=null, string json = null);
        Task LogInformation(string message = null, string reference = null, string json = null);
        Task LogWarning(string message, string reference = null, string json = null);
        Task LogWarning(Exception e, string message, string reference = null, string json = null);
        Task LogError(string message, string reference = null, string json = null);
        Task LogError(Exception e,string message,string reference);
        Task LogError(Exception e,string message=null);
        Task LogError(Exception e, string message, string reference = null, string json = null);
        Task LogCritical(string message, string reference = null, string json = null);
        Task LogCritical(Exception e, string message, string reference = null, string json = null);
    }
}
