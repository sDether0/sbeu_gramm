using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.Middleware.Repositories.Interfaces;

using Serilog;

namespace SBEU.Gramm.Middleware.Repositories
{
    public class NStoryRepository : BaseRepository<NStory>, INStoryRepository
    {
        public NStoryRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        /// <summary>
        /// It updates a story with the given id with the given story
        /// </summary>
        /// <param name="story"></param>
        /// <param name="id">The id of the story to be updated</param>
        /// <param name="userId"></param>
        /// <param name="NStory">The model</param>
        /// <returns>
        /// The updated story.
        /// </returns>
        public override async Task<NStory> Update(NStory story, string id, string userId)
        {
            _logger.Information($"Start of {nameof(Update)} in {GetType().Name} with id");
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.Error($"Parameter {nameof(id)} is null or whitespace");
                    throw Exceptions.InvalidArgument<NStory>();
                }
                if (story == null)
                {
                    _logger.Error("Edit model for story is null");
                    throw Exceptions.NullEditArgument();
                }
                var nStory = await _context.Stories.FindAsync(id);
                if (nStory == null)
                {
                    _logger.Error($"Story with id {id} is not found");
                    throw Exceptions.EntityNotFound<NStory>();
                }
                if (nStory.Author.Id != userId)
                {
                    _logger.Error($"Cannot edit story which author is not you");
                    throw Exceptions.InvalidArgument<User>();
                }
                if (nStory.IsDeleted)
                {
                    _logger.Error($"Story with id {id} is deleted");
                    throw Exceptions.EntityDeleted<NStory>();
                }
                nStory.TaggedUsers = story.TaggedUsers?.Count > 0 ? story.TaggedUsers : nStory.TaggedUsers;
                nStory.Description = story.Description ?? nStory.Description;
                nStory.Content = story.Content ?? nStory.Content;
                nStory.Fill(_context);
                _context.Stories.Update(nStory);
                await _context.SaveChangesAsync();
                _logger.Information($"Return updated story with id {id}");
                return nStory;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(Update)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }

                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(Update)} in {GetType().Name}");
            }
        }

        /// <summary>
        /// It creates a story and assigns the author to the story
        /// </summary>
        /// <param name="NStory">The model that I'm trying to create</param>
        /// <param name="userId">The id of the user who is creating the story</param>
        /// <returns>
        /// The story that was created.
        /// </returns>
        public async Task<NStory> Create(NStory story, string userId)
        {
            _logger.Information($"Start of {nameof(Create)} in {GetType().Name} with id");
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    _logger.Error($"{nameof(User)} \"self\" with id {userId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }
                story.Author = user;
                var result =await Create(story);
                _logger.Information($"Return created story with id {result.Id} and author id {userId}");
                return result;
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
        /// It adds a user to the list of users who have watched a story
        /// </summary>
        /// <param name="id">The id of the story to be watched</param>
        /// <param name="userId">The user who is watching the story</param>
        /// <returns>
        /// A boolean value.
        /// </returns>
        public async Task<bool> Watch(string id, string userId)
        {
            _logger.Information($"Start of {nameof(Watch)} in {GetType().Name} with id");
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.Error($"Parameter {nameof(id)} is null or whitespace");
                    throw Exceptions.InvalidArgument<NStory>();
                }
                var nStory = await _context.Stories.FindAsync(id);
                if (nStory == null)
                {
                    _logger.Error($"Story with id {id} is not found");
                    throw Exceptions.EntityNotFound<NStory>();
                }

                if (nStory.IsDeleted)
                {
                    _logger.Error($"Story with id {id} is deleted");
                    throw Exceptions.EntityDeleted<NStory>();
                }
                var watch = await _context.Users.FindAsync(userId);
                if (watch == null)
                {
                    _logger.Error($"{nameof(User)} \"self\" with id {userId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }
                nStory.WatchedUsers.Add(watch);
                _context.Update(nStory);
                await _context.SaveChangesAsync();
                _logger.Information($" Successfully marked story with id {id} as watched by user with id {userId}");
                return true;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(Watch)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }

                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(Watch)} in {GetType().Name}");
            }
        }

        /// <summary>
        /// It returns all the stories of the people the user is following, that are not deleted and
        /// that are less than 24 hours old
        /// </summary>
        /// <param name="userId">The id of the user who is logged in</param>
        /// <returns>
        /// A list of stories that are not deleted and are less than 24 hours old.
        /// </returns>
        public async Task<IEnumerable<NStory>> GetAll(string userId)
        {
            _logger.Information($"Start of {nameof(GetAll)} in {GetType().Name} with id");
            try
            {
                var user = await _context.Users.FindAsync(userId);
                var result = _context.Stories.Where(x => user.Following.Contains(x.Author) && !x.IsDeleted && (DateTime.UtcNow - x.CreatedAt).Hours < 24);
                _logger.Information($"Return {result.Count()} stories");
                return result;
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
    }
}
