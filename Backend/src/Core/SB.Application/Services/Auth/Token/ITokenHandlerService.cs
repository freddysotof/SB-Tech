using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Auth.Token
{
    public interface ITokenHandlerService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims);

        RefreshToken GenerateRefreshToken(int tokenValidityInHours);

        ClaimsPrincipal GetPrincipalFromToken(string token);

        ClaimsPrincipal GetPrincipalFromToken();

        string GetAuthorizationToken();
    }
}
