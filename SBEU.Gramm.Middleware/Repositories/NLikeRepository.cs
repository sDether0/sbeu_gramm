using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.DataLayer.DataBase.Entities.Base;
using SBEU.Gramm.DataLayer.Migrations;
using SBEU.Gramm.Middleware.Repositories.Interfaces;

using Serilog;

namespace SBEU.Gramm.Middleware.Repositories
{
    public class NLikeRepository : BaseRepository<NLike>, INLikeRepository
    {
        public NLikeRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        /// <summary>
        /// > This function updates a like in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id">The id of the entity to update</param>
        /// <param name="userId"></param>
        /// <param name="NLike">The type of the entity</param>
        /// <returns>
        /// The entity that was passed in.
        /// </returns>
        public override async Task<NLike> Update(NLike entity, string id, string userId)
        {
            await Task.Delay(10);
            return entity;
        }

        /// <summary>
        /// > Get all the likes for a given contentId
        /// </summary>
        /// <param name="contentId">The id of the content that you want to get the likes for.</param>
        /// <returns>
        /// A list of NLike objects.
        /// </returns>
        public async Task<IEnumerable<NLike>> GetByContentId(string contentId)
        {
            _logger.Information($"Start of {nameof(GetByContentId)} in {GetType().Name}");
            try
            {
                await _context.Users.LoadAsync();
                var content = await _context.FindEntity(contentId);
                var likes = content.GetLikes(_context);
                return likes;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(GetByContentId)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }

                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(GetByContentId)} in {GetType().Name}");
            }
        }

        /// <summary>
        /// > Create a new like for a given contentId and userId
        /// </summary>
        /// <param name="userId">The user who liked the content</param>
        /// <param name="contentId">The id of the content that is being liked.</param>
        /// <returns>
        /// A like object
        /// </returns>
        public async Task<NLike> Create(string userId, string contentId)
        {
            _logger.Information($"Start of {nameof(Create)} in {GetType().Name}");
            try
            {
                var likable = await _context.FindEntity(contentId);
                if (likable.Likes.Any(x => x.Author.Id == userId))
                {
                    _logger.Error($"Like from user with id {userId} for content with id {contentId} already exists");
                    throw Exceptions.AlreadyExist<NLike>();
                }
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    _logger.Error($"{nameof(User)} \"self\" not found");
                    throw Exceptions.EntityNotFound<User>();
                }
                var like = new NLike
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = user
                };
                likable.Likes.Add(like);
                _context.Update(likable);
                await _context.SaveChangesAsync();
                _logger.Information($"Successfully created Like from user with id {userId} for content with id {contentId}");
                return like;
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
        /// > Delete a like from a content
        /// </summary>
        /// <param name="userId">The user who liked the content</param>
        /// <param name="contentId">The id of the content that the user liked</param>
        /// <returns>
        /// A boolean value.
        /// </returns>
        public async Task<bool> Delete(string userId, string contentId)
        {
            _logger.Information($"Start of {nameof(Delete)} in {GetType().Name}");
            try
            {
                var content = await _context.FindEntity(contentId);
                var like = content.Likes.FirstOrDefault(x => x.Author.Id == userId);
                if (like == null)
                {
                    _logger.Error($"Like from user with id {userId} for content with id {contentId} is not found");
                    throw Exceptions.EntityNotFound<NLike>();
                }
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(Delete)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
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
