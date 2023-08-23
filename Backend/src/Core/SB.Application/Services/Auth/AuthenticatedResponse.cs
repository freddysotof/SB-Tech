using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.Application.Services.Auth.Token;

namespace SB.Application.Services.Auth
{
    public class AuthenticatedResponse
    {
        public AuthenticatedUser User { get; set; }

        public TokenInfo Credentials { get; set; }
    }
}
