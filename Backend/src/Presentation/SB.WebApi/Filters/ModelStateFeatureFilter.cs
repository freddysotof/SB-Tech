using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SB.Domain.Common.Models;

namespace SB.WebApi.Filters
{
    public class ModelStateFeatureFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var state = context.ModelState;
            context.HttpContext.Features.Set<ModelStateFeature>(new ModelStateFeature(state));
            await next();
        }
    }
    public class ModelStateFeatureAction : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            var state = context.ModelState;
            context.HttpContext.Features.Set(new ModelStateFeature(state));
            return Task.CompletedTask;
        }
    }
}
