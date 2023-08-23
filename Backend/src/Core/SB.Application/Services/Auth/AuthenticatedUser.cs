using SB.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Auth
{
    public class AuthenticatedUser
    {

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; } = "";


        public string Role { get; set; }


        public int? StatusId { get; set; }

        public AuthenticatedUser()
        {
        }

        public AuthenticatedUser(string id, string username, string name)
        {
            Username = username;
            Name = name;
        }



        public static implicit operator AuthenticatedUser(ClaimsIdentity claimsIdentity)
        {
            return new AuthenticatedUser
            {
                Username = claimsIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value,
                Name = claimsIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value,
                Role = claimsIdentity.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value,

            };
        }
    }
}
