using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SB.Application.Services.Auth;
using SB.Application.Services.Auth.AccessTokens;
using SB.Domain.Common.Models;
using SB.WebApi.Controllers.Base;
using UnauthorizedAccessException = SB.Domain.Exceptions.UnauthorizedAccessException;

namespace SB.WebApi.Controllers
{

    public class AuthController : ApiBaseAuthorizationController<AuthController>
    {
        private readonly IAuthService _authService;
        public AuthController(IServiceProvider serviceProvider,IAuthService authService)
     : base(serviceProvider)
        {
            _authService = authService;
        }

        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Customers Array</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthUser), StatusCodes.Status200OK, "application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] PetitionHandler<AuthCredential> petition)
        {
            //throw new BadRequestException("No aplica");
            var authResult = await _authService.AuthenticateAsync(petition.Data.Email,petition.Data.Password) ?? throw new UnauthorizedAccessException("Credenciales incorrectas");

            return Ok(authResult);
        }

        [HttpPost]
        [Route("token/renew")]
        [AllowAnonymous]
        public async Task<IActionResult> RenewAccessTokenAsync([FromBody] PetitionHandler<RefreshAccessToken> petition)
        {
            var authResult = await _authService.RenewAccessTokenAsync(petition.Data);
            return Ok(authResult);

        }

        [HttpPost]
        [Route("token/revoke/{username}")]

        public async Task<IActionResult> RevokeAsync(string username)
        {
            await _authService.RevokeAccessTokenAsync(username);
            return NoContent();
        }

    }
}
