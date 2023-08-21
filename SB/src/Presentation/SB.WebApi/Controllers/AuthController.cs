using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SB.Application.Services.Auth;
using SB.Domain.Exceptions;
using SB.WebApi.Configurations;
using SB.WebApi.Controllers.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SB.WebApi.Controllers
{

    public class AuthController : ApiBaseController<AuthController>
    {
        private readonly IAuthService _authService;
        private readonly AuthSettings _authSettings;
        public AuthController(IServiceProvider serviceProvider,IAuthService authService,IOptions<AuthSettings> options)
     : base(serviceProvider)
        {
            _authService = authService;
            _authSettings = options.Value;
        }

        [AllowAnonymous]
        [HttpPost("google/{tokenId}")]
        public async Task<IActionResult> Google(string tokenId)
        //public async Task<IActionResult> Google([FromBody] UserDTO userView)
        {
            try
            {
                //SimpleLogger.Log("userView = " + userView.tokenId);
                var payload = GoogleJsonWebSignature.ValidateAsync(tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
                var user = await _authService.Authenticate(payload);
                //SimpleLogger.Log(payload.ExpirationTimeSeconds.ToString());

                var claims = new[]
                {
                    //new Claim(JwtRegisteredClaimNames.Sub, Security.Encrypt(_authSettings.JwtEmailEncryption,user.email)),
                    new Claim(JwtRegisteredClaimNames.Sub, user.email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.JwtSecret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(String.Empty,
                  String.Empty,
                  claims,
                  expires: DateTime.Now.AddSeconds(55 * 60),
                  signingCredentials: creds);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
                //return Ok();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.GetBaseException().Message);
            }
        }
    }
}
