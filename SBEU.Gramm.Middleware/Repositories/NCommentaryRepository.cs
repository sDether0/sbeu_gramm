using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.EntityFrameworkCore;

using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.Middleware.Repositories.Interfaces;

using Serilog;

namespace SBEU.Gramm.Middleware.Repositories
{
    public class NCommentaryRepository : BaseRepository<NCommentary>, INCommentaryRepository
    {
        public NCommentaryRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        /// <summary>
        /// It updates a commentary in the database
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="id">the id of the commentary to be updated</param>
        /// <param name="userId"></param>
        /// <param name="NCommentary">is the model class</param>
        /// <returns>
        /// The updated commentary.
        /// </returns>
        public override async Task<NCommentary> Update(NCommentary comment, string id, string userId)
        {
            _logger.Information($"Start of {nameof(Update)} {nameof(NCommentary)} in {GetType().Name} with id");
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.Error($"Parameter {nameof(id)} is null or whitespace");
                    throw Exceptions.InvalidArgument<NCommentary>();
                }

                if (comment == null)
                {
                    _logger.Error("Edit model for comment is null");
                    throw Exceptions.NullEditArgument();
                }
                var nComment = await _context.Commentaries.FindAsync(id);
                if (nComment == null)
                {
                    _logger.Error($"Comment with id {id} is not found");
                    throw Exceptions.EntityNotFound<NCommentary>();
                }

                if (comment.IsDeleted)
                {
                    _logger.Error($"Comment with id {id} is deleted");
                    throw Exceptions.EntityDeleted<NCommentary>();
                }
                nComment.Text = comment.Text;
                _context.Commentaries.Update(nComment);
                await _context.SaveChangesAsync();
                return nComment;
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
        /// It returns all the comments that are not deleted and that belong to a post with the given id
        /// </summary>
        /// <param name="postid">the id of the post</param>
        /// <returns>
        /// IEnumerable<NCommentary>
        /// </returns>
        public async Task<IEnumerable<NCommentary>> GetByPost(string postid)
        {
            _logger.Information($"Start of {nameof(GetByPost)} {nameof(NCommentary)} in {GetType().Name} with id");
            try
            {
                await _context.Commentaries.LoadAsync();
                var comments = _context.Commentaries.Where(x => !x.IsDeleted && x.Post.Id == postid);
                _logger.Information($"Return {comments.Count()} commentaries for post with id {postid}");
                return comments;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(GetByPost)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }

                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(GetByPost)} in {GetType().Name}");
            }
        }

        /// <summary>
        /// > This function creates a new commentary and assigns the user who created it as the author
        /// </summary>
        /// <param name="NCommentary">is the model that I'm trying to create.</param>
        /// <param name="userId">the id of the user who is creating the comment</param>
        /// <returns>
        /// The return type is NCommentary.
        /// </returns>
        public async Task<NCommentary> Create(NCommentary comment, string userId)
        {
            _logger.Information($"Start of {nameof(Create)} {nameof(NCommentary)} in {GetType().Name} with id");
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    _logger.Error($"{nameof(User)} with id {userId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }
                comment.Author = user;
                var result = await Create(comment);
                _logger.Information($"Successfully created a post with author id {userId}");
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

    }
}
