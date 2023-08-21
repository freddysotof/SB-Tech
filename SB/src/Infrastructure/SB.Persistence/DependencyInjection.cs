using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.Persistence.Extensions;
using SB.Application.Abstractions.Repositories.Common;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using SB.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace SB.Persistence
{
    public static class DependencyInjection
    {
        public static void AddInfraestructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<SBDbContext>(opt => 
            opt.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            AddRepositoryToIoC(services, Assembly.GetExecutingAssembly());
        }

        //Method that enables automatic addition of repositories to IoC Container
        private static void AddRepositoryToIoC(IServiceCollection services, Assembly assembly)
        {
            var repositories = assembly.GetTypes().Where(x => x.IsAssignableToGenericType(typeof(IBaseEntityRepository<>)) && !x.IsGenericType);
            foreach (var item in repositories)
            {
                var @interface = item.GetInterfaces().FirstOrDefault(x => !x.IsGenericType) ?? throw new ArgumentNullException();
                services.AddScoped(@interface, item);
            }
        }
        //Function that checks whether a given type is implementing a generic interface
        private static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            return givenType.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == genericType) ||
                   givenType.BaseType != null && (givenType.BaseType.IsGenericType && givenType.BaseType.GetGenericTypeDefinition() == genericType ||
                                                  givenType.BaseType.IsAssignableToGenericType(genericType));
        }
    }
}
