using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Auth.AccessTokens
{
    public class RefreshAccessToken
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }
    }
}
