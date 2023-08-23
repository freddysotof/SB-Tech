using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Auth.AccessTokens
{
    public class UpdateAccessToken
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime? TokenExpiryTime { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public int? StatusId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
