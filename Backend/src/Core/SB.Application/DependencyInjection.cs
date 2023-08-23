using Microsoft.Extensions.DependencyInjection;
using SB.Application.Services.Auth;
using SB.Application.Services.Auth.AccessTokens;
using SB.Application.Services.Auth.Token;
using SB.Application.Services.Orders;
using SB.Application.Services.Products;

namespace SB.Application
{
    public static class DependencyInjection
    {
        // This is the function that will add the services you will use in this project to the IoC mechanism.
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenHandlerService, TokenHandlerService>();
            services.AddTransient<IAccessTokenService, AccessTokenService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
        }
     
    }
}
