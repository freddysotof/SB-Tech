using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace SB.Persistence.Extensions
{
    public static class PersistenceExtensions
    {
        public static async Task<IEnumerable<SqlParameter>> ExecuteSqlAsync(this DbContext context, string procedureName = null, List<SqlParameter> sqlParameters = null)
        {
            string sqlRaw = $"{procedureName} {sqlParameters.JoinSqlParameters()}";
            //int response;
            if (sqlParameters == null)
                await context.Database.ExecuteSqlRawAsync(sqlRaw);
            else
                await context.Database.ExecuteSqlRawAsync(sqlRaw, sqlParameters);
            return sqlParameters.Where(x => x.Direction == ParameterDirection.Output);
        }
        public static string JoinSqlParameters(this List<SqlParameter> sqlParameters)
        {
            List<string> parameters = new();
            if (sqlParameters != null && sqlParameters.Any())
                foreach (var param in sqlParameters)
                {
                    var parameterName = param.ParameterName;
                    parameterName += $"{((param.Direction == ParameterDirection.Output) ? " Output" : null)}";
                    parameters.Add(parameterName);
                }
            return string.Join(",", parameters);
        }
      
    }
}
