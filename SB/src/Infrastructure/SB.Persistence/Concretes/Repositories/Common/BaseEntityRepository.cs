using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SB.Application.Abstractions.Repositories.Common;
using SB.Domain.Entities.Common;
using SB.Persistence.Extensions;
using System.Data;

namespace SB.Persistence.Concretes.Repositories.Common
{
    /// <summary>
    /// Repositorio genérico para las funcionalidades comunes con la base de datos. 
    /// </summary>
    /// <typeparam name="TEntity">Cualquier clase que heredé de Entity.</typeparam>
    public class BaseEntityRepository<TEntity, TContext> : IBaseEntityRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _collections;
        private readonly IHttpContextAccessor _contextAccessor;
        /// <summary>
        /// Inicializa nuevas instancias según se vayan requiriendo.
        /// </summary>
        public BaseEntityRepository(IHttpContextAccessor httpContextAccessor, TContext context)
        {
            _context = context;
            _collections = context.Set<TEntity>();
            _contextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// La clase Queryable proporciona una implementación de los operadores de consulta estándar para
        /// consultar fuentes de datos que implementan 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Queryable(CancellationToken cancellationToken = default)
        {
            return _collections.AsQueryable();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_collections.AsEnumerable());
        }

        /// <summary>
        /// Obtener el registro por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _collections.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_collections.Where(x => ids.Contains((int)x.Id)).AsEnumerable());
        }

        public virtual async Task<TEntity> GetOneAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _collections.FindAsync(id);
        }


        /// <summary>
        /// Agregar el registro en la almacén de respaldo.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.CreatedDate = DateTime.Now;
            if (_contextAccessor != null && string.IsNullOrEmpty(entity.CreatedBy))
                entity.CreatedBy = _contextAccessor.HttpContext.User.Identity.Name;

            cancellationToken.ThrowIfCancellationRequested();

            _context.Add(entity);

            await SaveChangesAsync(cancellationToken);
            return entity;

        }

        /// <summary>
        /// Agrega registro de manera masiva.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
                //entity.DeletedToken = Guid.Empty.ToString();

                cancellationToken.ThrowIfCancellationRequested();

                _context.Add(entity);
            }

            await SaveChangesAsync(cancellationToken);
            return entities;
        }

        /// <summary>
        /// Actualiza el registro especificado.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {


            entity.UpdatedDate = DateTime.Now;
            if (_contextAccessor != null)
            {
                // detach
                _context.Entry(entity).State = EntityState.Detached;
                entity.UpdatedBy = _contextAccessor.HttpContext?.User.Identity.Name;
            }

            cancellationToken.ThrowIfCancellationRequested();

            _context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync(cancellationToken);

            return entity;
        }
        public virtual async Task<TEntity> UpdateAsync(int id, CancellationToken cancellationToken = default)
        {
            var remoteEntity = await GetOneAsync(id, cancellationToken);
            remoteEntity.UpdatedDate = DateTime.Now;
            if (_contextAccessor != null)
            {
                // detach
                _context.Entry(remoteEntity).State = EntityState.Detached;
                remoteEntity.UpdatedBy = _contextAccessor.HttpContext?.User.Identity.Name;
            }

            cancellationToken.ThrowIfCancellationRequested();

            _context.Entry(remoteEntity).State = EntityState.Modified;
            await SaveChangesAsync(cancellationToken);

            return remoteEntity;
        }


        /// <summary>
        /// Eliminar en registro base la estragia soft-delete.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {

            var entity = await UpdateAsync(id, cancellationToken);
            if (entity == null)
            {
                return false;
            }
            cancellationToken.ThrowIfCancellationRequested();

            entity.IsDeleted = true;

            _context.Entry(entity).State = EntityState.Detached;
            _collections.Remove(entity);
            await SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <inheritdoc />
        public virtual async Task<int> DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                var remoteEntity = await UpdateAsync((int)entity.Id, cancellationToken);
                if (remoteEntity == null)
                {
                    continue;
                }
                cancellationToken.ThrowIfCancellationRequested();

                entity.IsDeleted = true;

                _context.Entry(entity).State = EntityState.Detached;
                _collections.Remove(entity);
            }

            await SaveChangesAsync(cancellationToken);

            return entities.Count;
        }

        /// <summary>
        /// Guarda de forma asincrónica todos los cambios realizados
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<bool> ChangeStatusAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var remoteEntity = await _collections.FindAsync(entity.Id, cancellationToken);
            remoteEntity.StatusId = entity.StatusId;
            remoteEntity.UpdatedDate = DateTime.Now;
            if (_contextAccessor != null)
                remoteEntity.UpdatedBy = _contextAccessor.HttpContext.User.Identity.Name;

            _context.Entry(remoteEntity).State = EntityState.Modified;
            await SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<IEnumerable<SqlParameter>> ExecuteSqlAsync(string procedureName = null, List<SqlParameter> sqlParameters = null, CancellationToken cancellationToken = default)
        {
            string sqlRaw = $"{procedureName} {sqlParameters.JoinSqlParameters()}";
            //int response;
            if (sqlParameters == null)
                await _context.Database.ExecuteSqlRawAsync(sqlRaw, cancellationToken);
            else
                await _context.Database.ExecuteSqlRawAsync(sqlRaw, sqlParameters, cancellationToken);
            return sqlParameters.Where(x => x.Direction == ParameterDirection.Output);
        }
        public virtual async Task<List<TEntity>> ExecuteFromSqlRaw(string procedureName, List<SqlParameter> sqlParameters = null, CancellationToken cancellationToken = default)
        {
            List<TEntity> result;
            if (sqlParameters == null)
                result = await _collections.FromSqlRaw($"exec {procedureName}").ToListAsync(cancellationToken);
            else
                result = await _collections.FromSqlRaw($"exec {procedureName} {sqlParameters.JoinSqlParameters()}", sqlParameters?.ToArray())
                  .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<List<GenericEntity>> ExecuteFromSqlRaw<GenericEntity>(string procedureName, List<SqlParameter> sqlParameters = null, CancellationToken cancellationToken = default) where GenericEntity : class
        {
            DbSet<GenericEntity> genericCollections = _context.Set<GenericEntity>();
            List<GenericEntity> result;
            if (sqlParameters == null)
                result = await genericCollections.FromSqlRaw($"exec {procedureName}").ToListAsync(cancellationToken);
            else
                result = await genericCollections.FromSqlRaw($"exec {procedureName} {sqlParameters.JoinSqlParameters()}", sqlParameters?.ToArray())
                  .ToListAsync(cancellationToken);

            return result;
        }

        public Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetOneAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
