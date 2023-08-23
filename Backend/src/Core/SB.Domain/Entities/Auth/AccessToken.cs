using SB.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain.Entities.Auth
{
    public class AccessToken : BaseEntity
    {
        public new int Id { get; set; }

        public string Email { get; set; }

        public string? Token { get; set; }

        public DateTime? TokenExpiryTime { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public new string CreatedBy { get; set; }

        public new DateTime CreatedDate { get; set; } = DateTime.Now;


        public new string? UpdatedBy { get; set; }

        public new DateTime? UpdatedDate { get; set; }

        public new int? StatusId { get; set; }

        public new bool IsDeleted { get; set; }

        public AccessToken()
        {
        }

        public AccessToken(string email)
        {
            Email = email;
        }

        public AccessToken(string email, string token, DateTime expiration, string userId)
        {
            Email = email;
            Token = token;
            TokenExpiryTime = expiration;
            CreatedBy = userId;
            UpdatedBy = userId;
            UpdatedDate = DateTime.Now;
        }
    }
}
