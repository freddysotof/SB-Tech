using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Newtonsoft.Json;
using SB.WebApi.Filters;
using SB.WebApi.Helpers;
using SB.WebApi.Services;

namespace SB.WebApi
{
    public static class DependencyInjection
    {

        public static void AddResponseWrapper(this IServiceCollection services)
        {
            services.AddSingleton<IPageUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext?.Request;
                string uri = string.Empty;
                if (request != null)
                    uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new PageUriService(uri);
            });

            services.AddSingleton(typeof(IResponseWrapperService<>), typeof(ResponseWrapperService<>));

        }

        public static void ConfigureCustomModelStateValidation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opts =>
            {
                opts.InvalidModelStateResponseFactory = ctx => new ModelStateFeatureAction();
            });
        }

        public static void ConfigureApiControllers(this IServiceCollection services, bool memberCasing = false, bool apiVersioning = true)
        {
            if (apiVersioning)
            {
                services.AddVersionedApiExplorer(delegate (ApiExplorerOptions options)
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });
                services.AddApiVersioning(delegate (ApiVersioningOptions options)
                {
                    options.ReportApiVersions = true;
                });
            }

            services.AddControllers(delegate (MvcOptions options)
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            }).AddNewtonsoftJson(delegate (MvcNewtonsoftJsonOptions options)
            {
                if (memberCasing)
                {
                    options.UseMemberCasing();
                }

                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).AddJsonOptions(delegate (JsonOptions options)
            {
                options.JsonSerializerOptions.MaxDepth = int.MaxValue;
                options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }
    }
}
