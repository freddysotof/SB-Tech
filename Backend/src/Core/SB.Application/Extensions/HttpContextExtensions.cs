using Microsoft.AspNetCore.Http;
using SB.Application.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetCurrentUserName(this IHttpContextAccessor contextAccessor)
     => $"{contextAccessor.HttpContext?.User?.Identity?.Name}";

     

        public static IEnumerable<Claim>? GetUserClaims(this IHttpContextAccessor contextAccessor)
        => contextAccessor.HttpContext?.User?.Claims;
 
    }
}
