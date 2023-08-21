using Microsoft.AspNetCore.Mvc;
using SB.WebApi.Controllers.Base;
using SB.WebApi.Wrappers;

namespace SB.WebApi.Controllers
{

    public class ExampleController : ApiBaseController<ExampleController>
    {
        public ExampleController(IServiceProvider serviceProvider)
       : base(serviceProvider)
        {

        }


        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Customers Array</returns>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponseWrapper<>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> SuccessExampleAsync()
        {
            return Ok(new { Response="Ok"});
        }

        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Customers Array</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponseWrapper<>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> ErrorExampleAsync()
        {
            throw new BadHttpRequestException("Error");
        }

    }
}
