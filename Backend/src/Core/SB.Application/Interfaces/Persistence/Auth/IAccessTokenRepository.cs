using SB.Application.Interfaces.Persistence.Common;
using SB.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Interfaces.Persistence.Auth
{
    public interface IAccessTokenRepository : IBaseEntityRepository<AccessToken>
    {
        Task<AccessToken> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
