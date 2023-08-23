using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Auth.Token
{
    public class TokenInfo
    {
        public AuthToken AccessToken { get; set; }

        public RefreshToken RefreshToken { get; set; }
    }
}
