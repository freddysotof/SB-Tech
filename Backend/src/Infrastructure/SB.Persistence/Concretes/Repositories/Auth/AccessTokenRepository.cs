using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SB.Application.Interfaces.Persistence.Auth;
using SB.Domain.Entities.Auth;
using SB.Persistence.Concretes.Repositories.Common;
using SB.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Persistence.Concretes.Repositories.Auth
{
    public class AccessTokenRepository : BaseEntityRepository<AccessToken, SBDbContext>, IAccessTokenRepository
    {
        private readonly DbSet<AccessToken> _collections;
        private readonly SBDbContext _context;
        public AccessTokenRepository(IHttpContextAccessor httpContextAccessor, SBDbContext dbContext )
            : base(httpContextAccessor, dbContext)
        {
            _context = dbContext;
            _collections = _context.Set<AccessToken>();
        }

        public async Task<AccessToken> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var data = await _collections.SingleOrDefaultAsync(x => x.Email == email);
            return data;
        }
    }
}
