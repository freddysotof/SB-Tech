using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Common
{
    public interface IGenericService<GetModel, CreateModel, UpdateModel> where GetModel : class where CreateModel : class where UpdateModel : class
    {
        Task<List<GetModel>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<GetModel> GetByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<GetModel> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task<GetModel> AddAsync(CreateModel create, CancellationToken cancellationToken = default(CancellationToken));

        Task<GetModel> UpdateAsync(int id, UpdateModel update, CancellationToken cancellationToken = default(CancellationToken));

        Task<GetModel> UpdateAsync(string id, UpdateModel update, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
