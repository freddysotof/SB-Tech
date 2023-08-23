using SB.Domain.Entities.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Auth.AccessTokens
{
    public class CreateAccessToken
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public DateTime? TokenExpiryTime { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public int? StatusId { get; set; }

        public static implicit operator AccessToken(CreateAccessToken create)
        {
            return new AccessToken
            {
                CreatedBy = create.Email,
                Email = create.Email,
                StatusId = create.StatusId,
                Token = create.Token,
                TokenExpiryTime = create.TokenExpiryTime,
                RefreshToken = create.RefreshToken,
                RefreshTokenExpiryTime = create.RefreshTokenExpiryTime
            };
        }
    }
}
