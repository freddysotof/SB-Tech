using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SB.Application.Abstractions.Repositories.Common;
using SB.Persistence.Context;
using System.Data;
using System.Reflection;

namespace SB.Persistence.Extensions
{
    public static class PersistenceServiceRegistration
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
            //return string.Join(",", parameters);
        }

        // Bu projede kullanacaðýnýz servisleri IoC mekanizmasýna ekleyecek olan fonksiyondur.
        // This is the function that will add the services you will use in this project to the IoC mechanism.
        //public static IServiceCollection AddPersistenceServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        //{
        //    //services.AddDbContext<ApplicaitonDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
        //    //AddRepositoryToIoC(services, Assembly.GetExecutingAssembly());
        //    //return services;
        //}

        // Repository lerin otomatik olarak IoC Container a eklenmesini saðlayan metod
       


        //Type in verilen generic türden türeyip türemediðini kontrol eden fonksiyon
      
    }
}
