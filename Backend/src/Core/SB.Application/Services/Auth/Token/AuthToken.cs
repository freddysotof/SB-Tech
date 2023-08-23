using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Auth.Token
{
    public class AuthToken
    {
        public string Token { get; set; }

        public DateTime? ExpiryTime { get; set; }
        public AuthToken(string token, DateTime? expiryTime)
        {
            Token = token;
            ExpiryTime = expiryTime;
        }
        public AuthToken()
        {

        }
    }
}
