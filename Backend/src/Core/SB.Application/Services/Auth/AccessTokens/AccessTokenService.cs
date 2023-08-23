using SB.Application.Interfaces.Persistence.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Auth.AccessTokens
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IAccessTokenRepository _accessTokenRepository;
        public AccessTokenService(IAccessTokenRepository accessTokenRepository)
        {
            _accessTokenRepository = accessTokenRepository;
        }

        public async Task<GetAccessToken> AddAsync(CreateAccessToken create, CancellationToken cancellationToken = default)
        {
            var createdAccessToken = await _accessTokenRepository.AddAsync(create, cancellationToken);
            return new GetAccessToken()
            {
                Id = createdAccessToken.Id,
                RefreshToken = createdAccessToken.RefreshToken,
                TokenExpiryTime = createdAccessToken.TokenExpiryTime,
                Token = createdAccessToken.Token,
                StatusId = createdAccessToken.StatusId,
                Email = createdAccessToken.Email,
                IsDeleted = createdAccessToken.IsDeleted,
                RefreshTokenExpiryTime = createdAccessToken.RefreshTokenExpiryTime
            };
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        => await _accessTokenRepository.DeleteAsync(id, cancellationToken);


        public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();


        public Task<List<GetAccessToken>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<GetAccessToken> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();
        public Task<GetAccessToken> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAccessToken> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var accessToken = await _accessTokenRepository.GetByEmailAsync(email, cancellationToken);
            if (accessToken == null)
                return null;
            return new GetAccessToken()
            {
                Id = accessToken.Id,
                RefreshToken = accessToken.RefreshToken,
                TokenExpiryTime = accessToken.TokenExpiryTime,
                Token = accessToken.Token,
                StatusId = accessToken.StatusId,
                Email = accessToken.Email,
                IsDeleted = accessToken.IsDeleted,
                RefreshTokenExpiryTime = accessToken.RefreshTokenExpiryTime
            };
        }
        public Task<GetAccessToken> UpdateAsync(string id, UpdateAccessToken update, CancellationToken cancellationToken = default)
      => throw new NotSupportedException();

        public async Task<GetAccessToken> UpdateAsync(int id, UpdateAccessToken update, CancellationToken cancellationToken = default)
        {
            var accessToken = await _accessTokenRepository.GetByIdAsync(id, cancellationToken);
            accessToken.RefreshToken = update.RefreshToken;
            accessToken.RefreshTokenExpiryTime = update.RefreshTokenExpiryTime;
            accessToken.TokenExpiryTime = update.TokenExpiryTime;
            accessToken.Token = update.Token;
            accessToken.UpdatedDate = DateTime.Now;

            await _accessTokenRepository.UpdateAsync(accessToken, cancellationToken);
            return new GetAccessToken()
            {
                Id = accessToken.Id,
                RefreshToken = accessToken.RefreshToken,
                TokenExpiryTime = accessToken.TokenExpiryTime,
                Token = accessToken.Token,
                StatusId = accessToken.StatusId,
                Email = accessToken.Email,
                IsDeleted = accessToken.IsDeleted,
                RefreshTokenExpiryTime = accessToken.RefreshTokenExpiryTime
            };
        }


    }
}
