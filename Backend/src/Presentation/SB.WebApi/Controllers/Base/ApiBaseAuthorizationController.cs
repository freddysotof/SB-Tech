using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SB.WebApi.Controllers.Base
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public abstract class ApiBaseAuthorizationController<T> : ControllerBase where T : ApiBaseAuthorizationController<T>
    {
        protected IHttpContextAccessor ContextAccessor { get; set; }

        protected ILoggerManager<T> Logger { get; set; }

        public ApiBaseAuthorizationController(IServiceProvider serviceProvider)
        {
            ContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            Logger = serviceProvider.GetRequiredService<ILoggerManager<T>>();
        }
    }
}
