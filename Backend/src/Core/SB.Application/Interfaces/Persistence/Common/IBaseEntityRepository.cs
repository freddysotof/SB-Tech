using Microsoft.Data.SqlClient;
using SB.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Interfaces.Persistence.Common
{
    public interface IBaseEntityRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// La clase Queryable proporciona una implementación de los operadores de consulta estándar para
        /// consultar fuentes de datos que implementan 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        IQueryable<T> Queryable(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Obtener el registro por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);


        /// <summary>
        /// Obtener el registro por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);

        Task<T> GetOneAsync(int id, CancellationToken cancellationToken = default);
        Task<T> GetOneAsync(string id, CancellationToken cancellationToken = default);

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Agrega registro de manera masiva.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> ChangeStatusAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Guarda de forma asincrónica todos los cambios realizados
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<List<T>> ExecuteFromSqlRaw(string procedureName, List<SqlParameter> sqlParameters = null, CancellationToken cancellationToken = default);
        Task<List<TEntity>> ExecuteFromSqlRaw<TEntity>(string procedureName, List<SqlParameter> sqlParameters = null, CancellationToken cancellationToken = default) where TEntity : class;
        Task<IEnumerable<SqlParameter>> ExecuteSqlAsync(string procedureName = null, List<SqlParameter> sqlParameters = null, CancellationToken cancellationToken = default);
    }
}
