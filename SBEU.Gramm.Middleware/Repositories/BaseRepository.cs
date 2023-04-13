using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.DataLayer.DataBase.Entities.Base;
using SBEU.Gramm.Middleware.Repositories.Interfaces;
using Serilog;
using ILogger = Serilog.ILogger;

namespace SBEU.Gramm.Middleware.Repositories
{
    /* It's a base class for all repositories that provides basic CRUD functionality */
    public abstract class BaseRepository<T> : Repository, IBaseRepository<T> where T : DeletableEntity
    {
        /* It's a field that is used to access the database. */
        
        /* It's a constructor that takes a parameter of type `ApiDbContext` and assigns it to the
        `_context` field. */
        protected BaseRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        /// <summary>
        /// > GetById() returns a single entity of type T from the database, or throws an exception if
        /// the entity is not found
        /// </summary>
        /// <param name="id">The id of the entity to be retrieved</param>
        /// <returns>
        /// The entity that was found.
        /// </returns>
        public async Task<T> GetById(string id)
        {
            _logger.Information($"Start of {nameof(GetById)} {typeof(T).Name} in {GetType().Name} with id {id}");
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _logger.Error($"Invalid parameter {nameof(id)}");
                    throw Exceptions.InvalidArgument<T>();
                }

                var entity = await _context.FindAsync<T>(id);
                if (entity == null)
                {
                    _logger.Error($"{typeof(T).Name} with id {id} not found");
                    throw Exceptions.EntityNotFound<T>();
                }

                _logger.Information($"Return {typeof(T).Name} with id {id}");
                return entity;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(GetById)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(GetById)} in {GetType().Name}");
            }
        }

        /// <summary>
        /// > Get all entities that are not deleted
        /// </summary>
        /// <returns>
        /// A list of entities that are not deleted.
        /// </returns>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            _logger.Information($"Start of {nameof(GetAll)} {typeof(T).Name} in {GetType().Name}");
            try
            {
                await _context.Set<T>().LoadAsync();
                var entities = _context.Set<T>().Where(x => !x.IsDeleted);
                _logger.Information($"Return {entities.Count()} {typeof(T).Name}s");
                return entities;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(GetAll)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(GetAll)} in {GetType().Name}");
            }
        }

        /// <summary>
        /// It creates a new entity and adds it to the database
        /// </summary>
        /// <param name="T">The entity type</param>
        /// <returns>
        /// The entity that was created.
        /// </returns>
        public async Task<T> Create(T entity)
        {
            _logger.Information($"Start of {nameof(Create)} {typeof(T).Name} in {GetType().Name} with parameter {typeof(T).Name}");
            try
            {
                if (entity == null)
                {
                    _logger.Error($"Parameter {typeof(T)} is null");
                    throw Exceptions.NullCreateArgument();
                }
                entity.Id = Guid.NewGuid().ToString();
                entity.Fill(_context);
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                _logger.Information($"Successfully created {typeof(T).Name} with id {entity.Id}");
                return entity;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(Create)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(Create)} in {GetType().Name}");
            }
        }

        /// <summary>
        /// > Update an entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id">The id of the entity to update.</param>
        /// <param name="userId"></param>
        /// <param name="T">The type of the entity.</param>
        public abstract Task<T> Update(T entity, string id, string userId);

        /// <summary>
        /// > This function deletes an entity by setting its IsDeleted property to true
        /// </summary>
        /// <param name="id">The id of the entity to be deleted</param>
        /// <returns>
        /// A boolean value.
        /// </returns>
        public async Task<bool> Delete(string id)
        {
            _logger.Information($"Start of {nameof(Delete)} {typeof(T).Name} in {GetType().Name} with id");
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.Error($"Invalid parameter {nameof(id)}");
                    throw Exceptions.InvalidArgument<T>();
                }
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    _logger.Error($"{typeof(T).Name} with id {id} not found");
                    throw Exceptions.EntityNotFound<T>();
                }
                entity.IsDeleted = true;
                _context.Update(entity);
                await _context.SaveChangesAsync();
                _logger.Information($"{typeof(T).Name} with id {id} successfully marked as deleted");
                return true;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(Delete)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message??"");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(Delete)} in {GetType().Name}");
            }
        }
    }
}
