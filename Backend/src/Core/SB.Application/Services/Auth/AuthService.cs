using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SB.Application.Services.Auth.AccessTokens;
using SB.Application.Services.Auth.Token;
using SB.Domain.Entities.Auth;
using SB.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SB.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ITokenHandlerService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthService(ITokenHandlerService tokenHandlerService, IConfiguration configuration, IAccessTokenService accessTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _tokenService = tokenHandlerService;
            _configuration = configuration;
            _accessTokenService = accessTokenService;
            _contextAccessor = httpContextAccessor;
            Refresh();
        }

        public async Task<AuthenticatedResponse> AuthenticateAsync(string email,string password)
        {
            var usr = FindUser(email);
            if (usr == null)
                throw new NotFoundException("Usuario no encontrado");
            if (usr.Password != password)
                throw new BadRequestException("Credenciales incorrectas");
            var user = new AuthenticatedUser
            {
                Email = email,
                Username = usr.Email,
                Name = usr.Name
            };
            return new AuthenticatedResponse
            {
                User = user,
                Credentials = await GenerateCredentials(user)
            };
        }

        private async Task<TokenInfo> GenerateCredentials(AuthenticatedUser user)
        {
            var authClaims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var token = _tokenService.GenerateAccessToken(authClaims);
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInHours"], out int refreshTokenValidityInHours);
            var refreshToken = _tokenService.GenerateRefreshToken(refreshTokenValidityInHours);

            TokenInfo tokenResult = new()
            {
                AccessToken = new AuthToken()
                {
                    ExpiryTime = token.ValidTo,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                },
                RefreshToken = refreshToken
            };

            var accessToken = await _accessTokenService.GetByEmailAsync(user.Email);
            if (accessToken == null)
            {
                _ = await _accessTokenService.AddAsync(new CreateAccessToken()
                {
                    RefreshToken = tokenResult.RefreshToken.Token,
                    RefreshTokenExpiryTime = tokenResult.RefreshToken.ExpiryTime,
                    Token = tokenResult.AccessToken.Token,
                    TokenExpiryTime = tokenResult.AccessToken.ExpiryTime,
                    Email = user.Email,
                });
            }
            else
            {
                await _accessTokenService.UpdateAsync(accessToken.Id, new UpdateAccessToken
                {
                    Id = accessToken.Id,
                    RefreshToken = tokenResult.RefreshToken.Token,
                    RefreshTokenExpiryTime = tokenResult.RefreshToken.ExpiryTime,
                    TokenExpiryTime = tokenResult.AccessToken.ExpiryTime,
                    Token = tokenResult.AccessToken.Token,
                    IsDeleted = accessToken.IsDeleted,
                    StatusId = accessToken.StatusId

                });
            }
            return tokenResult;
        }

     

        public async Task<AuthenticatedResponse> RenewAccessTokenAsync(RefreshAccessToken refreshAccessToken)
        {
            if (refreshAccessToken.AccessToken is null || refreshAccessToken.RefreshToken is null)
            {
                throw new BadRequestException("Invalid parameters");
            }

            string? refreshToken = refreshAccessToken.RefreshToken;
            string? token = refreshAccessToken.AccessToken;
            //string authHeader = _contextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization];
            //_ = AuthenticationHeaderValue.TryParse(authHeader, out var headerValue);
            //if(headerValue == null)
            //    throw new UnauthorizedAccessException("Not authorized");
            var principal = _tokenService.GetPrincipalFromToken(token) ?? throw new BadRequestException("Invalid access token or refresh token");


            string email = principal.FindFirst(ClaimTypes.Email).Value;

            var accessToken = await _accessTokenService.GetByEmailAsync(email);

            if (accessToken == null || accessToken.RefreshToken != refreshToken || accessToken.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new BadRequestException("Invalid access token or refresh token");
            }
            TokenInfo tokenResult = null;
            if (accessToken.TokenExpiryTime < DateTime.Now)
            {
                var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList());
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInHours"], out int refreshTokenValidityInHours);

                var newRefreshToken = _tokenService.GenerateRefreshToken(refreshTokenValidityInHours);

                tokenResult = new()
                {
                    AccessToken = new AuthToken()
                    {
                        ExpiryTime = newRefreshToken.ExpiryTime,
                        Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken)
                    },
                    RefreshToken = newRefreshToken
                };
                accessToken = await _accessTokenService.UpdateAsync(accessToken.Id, new UpdateAccessToken()
                {
                    Id = accessToken.Id,
                    IsDeleted = accessToken.IsDeleted,
                    RefreshToken = newRefreshToken.Token,
                    RefreshTokenExpiryTime = (DateTime)newRefreshToken.ExpiryTime,
                    Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    TokenExpiryTime = newAccessToken.ValidTo
                });

            }
            else
                tokenResult = new()
                {
                    AccessToken = new AuthToken()
                    {
                        ExpiryTime = accessToken.TokenExpiryTime,
                        Token = accessToken.Token
                    },
                    RefreshToken = new RefreshToken(accessToken.RefreshToken, accessToken.RefreshTokenExpiryTime)
                };




            var usr = FindUser(email);
            AuthenticatedUser user = new()
            {
                Username=email,
                Email=email,
                Name=usr.Name,
            };

            return new AuthenticatedResponse()
            {
                User = user,
                Credentials = tokenResult
            };

        }
      
        public async Task RevokeAccessTokenAsync(string email)
        {
            var accessToken = await _accessTokenService.GetByEmailAsync(email) ?? throw new NotFoundException("User Token Not Found");
            await _accessTokenService.DeleteAsync(accessToken.Id);
        }

     
        private static IList<User> _users = new List<User>();
        private User FindUser(string email)
        {
            var u = _users.Where(x => x.Email == email).FirstOrDefault();
            return u;
        }
       

        private void Refresh()
        {
            if (_users.Count == 0)
            {
                _users.Add(new User() { Id = Guid.NewGuid(), Name = "Test Person1", Email = "testperson1@gmail.com",Password = "1234" });
                _users.Add(new User() { Id = Guid.NewGuid(), Name = "Test Person2", Email = "testperson2@gmail.com", Password = "1234" });
                _users.Add(new User() { Id = Guid.NewGuid(), Name = "Test Person3", Email = "testperson3@gmail.com", Password = "1234" });
            }
        }

      
    }
}
