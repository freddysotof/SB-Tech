using Microsoft.Extensions.DependencyInjection;
using SB.Application.Abstractions.Repositories.Common;
using SB.Application.Services.Auth;
using SB.Application.Services.Common;
using SB.Application.Services.Orders;
using SB.Application.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application
{
    public static class DependencyInjection
    {
        // This is the function that will add the services you will use in this project to the IoC mechanism.
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            //AddServicesToIoC(services, Assembly.GetExecutingAssembly());
        }
        //Method that enables automatic addition of services to IoC Container
        private static void AddServicesToIoC(IServiceCollection services, Assembly assembly)
        {
            var interfaces = assembly.GetTypes().Where(x => x.IsAssignableToGenericType(typeof(IGenericService<,,>)) && !x.IsGenericType);
            foreach (var item in interfaces)
            {
                var imter = item.GetInterfaces();
                //var @interface = item.GetInterfaces().FirstOrDefault(x => !x.IsGenericType) ?? throw new ArgumentNullException();
                //services.AddTransient(@interface, item);
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
