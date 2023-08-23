using SB.Application.Services.Common;

namespace SB.Application.Services.Auth.AccessTokens
{
    public interface IAccessTokenService : IGenericService<GetAccessToken, CreateAccessToken, UpdateAccessToken>
    {
        Task<GetAccessToken> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
