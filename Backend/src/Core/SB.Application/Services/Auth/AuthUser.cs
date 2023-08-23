using SB.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Auth
{
    public class AuthUser
    {

        public string Username { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; } = "";


        public string Role { get; set; }

        public int? StatusId { get; set; }

        public AuthUser()
        {
        }

        public AuthUser(string id, string username, string displayName)
        {
            Username= username;
            DisplayName = displayName;
        }

       

        public static implicit operator AuthUser(ClaimsIdentity claimsIdentity)
        {
            return new AuthUser
            {
                Username= claimsIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value,
                DisplayName= claimsIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value,
                Role = claimsIdentity.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value,
            };
        }
    }
}
