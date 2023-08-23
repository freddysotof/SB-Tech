using SB.Application.Services.Auth.AccessTokens;

namespace SB.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthenticatedResponse> AuthenticateAsync(string email,string password);
        Task<AuthenticatedResponse> RenewAccessTokenAsync(RefreshAccessToken model);
        Task RevokeAccessTokenAsync(string username);
    }

}
