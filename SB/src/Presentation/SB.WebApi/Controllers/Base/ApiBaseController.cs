using LoggerService;
using Microsoft.AspNetCore.Mvc;

namespace SB.WebApi.Controllers.Base
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiBaseController<T> : ControllerBase where T : ApiBaseController<T>
    {
        protected IHttpContextAccessor ContextAccessor { get; set; }

        protected ILoggerManager<T> Logger { get; set; }

        public ApiBaseController(IServiceProvider serviceProvider)
        {
            ContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            Logger = serviceProvider.GetRequiredService<ILoggerManager<T>>();
        }

        public ApiBaseController()
        {
        }
    }
}
