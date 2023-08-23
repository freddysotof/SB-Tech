using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Auth.Token
{
    public class RefreshToken : AuthToken
    {
        public RefreshToken(string token, DateTime? expiryTime)
        {
            Token = token;
            ExpiryTime = expiryTime;
        }
        public RefreshToken()
        {

        }
    }
}
