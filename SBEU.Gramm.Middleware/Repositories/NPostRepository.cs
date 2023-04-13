using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;

using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.DataLayer.DataBase.Entities.Relate;
using SBEU.Gramm.Middleware.Repositories.Interfaces;

using Serilog;

namespace SBEU.Gramm.Middleware.Repositories
{
    public class NPostRepository : BaseRepository<NPost>, INPostRepository
    {
        private readonly IUserRepository _users;
        public NPostRepository(ApiDbContext context, ILogger logger, IUserRepository users) : base(context, logger)
        {
            _users = users;
        }

        /// <summary>
        /// It updates a post with the given id with the given post
        /// </summary>
        /// <param name="post"></param>
        /// <param name="id">The id of the post to be updated</param>
        /// <param name="userId"></param>
        /// <param name="NPost">The model</param>
        /// <returns>
        /// The updated post.
        /// </returns>
        public override async Task<NPost> Update(NPost post, string id, string userId)
        {
            _logger.Information($"Start of {nameof(Update)} in {GetType().Name} ");
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.Error($"Parameter {nameof(id)} is null or whitespace");
                    throw Exceptions.InvalidArgument<NPost>();
                }

                if (post == null)
                {
                    _logger.Error("Edit model for post is null");
                    throw Exceptions.NullEditArgument();
                }
                var nPost = await _context.Posts.FindAsync(id);
                if (nPost == null)
                {
                    _logger.Error($"Post with id {id} is not found");
                    throw Exceptions.EntityNotFound<NPost>();
                }

                if (nPost.Author.Id != userId)
                {
                    _logger.Error($"Cannot edit post which author is not you");
                    throw Exceptions.InvalidArgument<User>();
                }
                if (nPost.IsDeleted)
                {
                    _logger.Error($"Post with id {id} is deleted");
                    throw Exceptions.EntityDeleted<NPost>();
                }
                nPost.TaggedUsers = post.TaggedUsers?.Count > 0 ? post.TaggedUsers : nPost.TaggedUsers;
                nPost.Description = post.Description ?? nPost.Description;
                nPost.Tags = post.Tags.Count > 0 ? post.Tags : nPost.Tags;
                nPost.Contents = post.Contents.Count > 0 ? post.Contents : nPost.Contents;
                nPost.Fill(_context);
                _context.Update(nPost);
                await _context.SaveChangesAsync();
                _logger.Information($"Return updated post with id {id}");
                return nPost;
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
        /// It returns a list of posts that are not deleted and the author of the post is not the user
        /// who is logged in
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="skip">the number of records to skip</param>
        /// <param name="take">The number of records to return.</param>
        /// <returns>
        /// IEnumerable<NPost>
        /// </returns>
        public async Task<IEnumerable<NPost>> GetAll(string UserId, int skip = 0, int take = 10)
        {
            _logger.Information($"Start of {nameof(GetAll)} in {GetType().Name}");
            try
            {
                var result = _context.Posts.Where(p => !p.IsDeleted && p.Author.Id != UserId).Skip(skip).Take(take);
                _logger.Information($"Return {result.Count()} posts, skipped {skip}");
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

        /// <summary>
        /// It takes a user's location and returns a list of 100 posts within a 50 mile radius of the
        /// user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="lat">latitude of the user</param>
        /// <param name="lng">longitude</param>
        /// <returns>
        /// A list of posts that are within a certain radius of the user.
        /// </returns>
        public async Task<IEnumerable<NPost>> GetRadius(string UserId, double lat, double lng)
        {
            _logger.Information($"Start of {nameof(GetRadius)}  in {GetType().Name} ");
            try
            {
                List<NPost> result = new(100);
                double radius = 1.0;
                while (result.Count < 100 && radius < 50)
                {
                    var posts = _context.Posts.ToList().Where(x => !x.IsDeleted && 
                        x.Author.Id != UserId & TwoPointDistance.Calculate(x.Latitude, x.Longitude, lat, lng) <= radius);
                    foreach (var post in posts)
                    {
                        if (result.Count < 100 && !result.Contains(post))
                        {
                            result.Add(post);
                        }
                    }
                    radius++;
                }
                _logger.Information($"Return {result.Count} posts within the radius of {radius} kilometers");
                return result;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(GetRadius)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }

                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(GetRadius)} in {GetType().Name}");
            }
        }


        /// <summary>
        /// It takes a post and a userId, finds the user with that id, sets the post's author to that
        /// user, and then creates the post
        /// </summary>
        /// <param name="NPost">The model that we're creating</param>
        /// <param name="userId">The id of the user who is creating the post.</param>
        /// <returns>
        /// The post is being returned.
        /// </returns>
        public async Task<NPost> Create(NPost post, string userId)
        {
            _logger.Information($"Start of {nameof(Create)} in {GetType().Name} ");
            try
            {
                var user = await _users.GetByIdAsync(userId);
                if (user == null)
                {
                    _logger.Error($"{nameof(User)} \"self\" with id {userId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }
                post.Author = user;
                var result = await Create(post);
                result.Contents.ToList().ForEach(x =>
                {
                    var pos = new XIdentityUserContent()
                    {
                        User = user,
                        Content = x,
                        Position = !user.ContentsPosition.IsNullOrEmpty()? user.ContentsPosition.Max(g => g.Position) + 1:1
                    };
                    user.ContentsPosition.Add(pos);
                });
                _context.Update(user);
                await _context.SaveChangesAsync();
                result.Tags.ToList().ForEach(x => _context.Entry(x).State = EntityState.Detached);
                _logger.Information($"Return created post with id {post.Id} and author id {userId}");
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
                    throw Exceptions.InvalidArgument<NPost>();
                }
                var nPost = await _context.Posts.FindAsync(id);
                if (nPost == null)
                {
                    _logger.Error($"Post with id {id} is not found");
                    throw Exceptions.EntityNotFound<NPost>();
                }

                if (nPost.IsDeleted)
                {
                    _logger.Error($"Post with id {id} is deleted");
                    throw Exceptions.EntityDeleted<NPost>();
                }
                var watch = await _context.Users.FindAsync(userId);
                if (watch == null)
                {
                    _logger.Error($"{nameof(User)} \"self\" with id {userId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }
                nPost.WatchedUsers.Add(watch);
                _context.Update(nPost);
                await _context.SaveChangesAsync();
                _logger.Information($" Successfully marked post with id {id} as watched by user with id {userId}");
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
    }
}
