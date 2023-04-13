using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.Middleware.Repositories.Interfaces;
using SBEU.Gramm.Middleware.SMTP;
using SBEU.Gramm.Models.Requests.Update;
using SBEU.Gramm.Models.Responses;

using Serilog;

namespace SBEU.Gramm.Middleware.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {

        private readonly UserManager<XIdentityUser> _userManager;
        public UserRepository(ApiDbContext context, ILogger logger, UserManager<XIdentityUser> userManager) : base(context, logger)
        {
            _userManager = userManager;
        }
        public async Task FollowAsync(string userId, string targetId)
        {
            _logger.Information($"Start of {nameof(FollowAsync)} {nameof(User)} in {GetType().Name} with userId {userId} and targetId {targetId}");
            try
            {
                var user = await GetByIdAsync(targetId);
                if (user == null)
                {
                    _logger.Error($"{nameof(User)} for follow with id {targetId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }

                var self = await GetByIdAsync(userId);
                if (self == null)
                {
                    _logger.Error($"{nameof(User)}  with id {userId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }
                self.Following.Add(user);
                _context.Update(self);
                await _context.SaveChangesAsync();
                _logger.Information($"Successfully follow {nameof(User)} with id {user.Id}");
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(FollowAsync)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(FollowAsync)} in {GetType().Name}");
            }
        }

        public async Task UnFollowAsync(string userId, string targetId)
        {
            _logger.Information($"Start of {nameof(UnFollowAsync)} {nameof(User)} in {GetType().Name} with userId {userId} and targetId {targetId}");
            try
            {
                var user = await GetByIdAsync(targetId);
                if (user == null)
                {
                    _logger.Error($"{nameof(User)} for unfollow with id {targetId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }

                var self = await GetByIdAsync(userId);
                if (self == null)
                {
                    _logger.Error($"{nameof(User)}  with id {userId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }
                self.Following.Remove(user);
                _context.Update(self);
                await _context.SaveChangesAsync();
                _logger.Information($"Successfully unfollow {nameof(User)} with id {user.Id}");
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(UnFollowAsync)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(UnFollowAsync)} in {GetType().Name}");
            }
        }

        public async Task<XIdentityUser> GetByIdAsync(string userId)
        {
            _logger.Information($"Start of {nameof(GetByIdAsync)}  in {GetType().Name} with userId {userId}");
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    _logger.Error($"{nameof(User)} with id {userId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }

                user.Posts = user.Posts.Where(x => !x.IsDeleted).ToList();
                _logger.Information($"Return {nameof(User)} with id {user.Id}");
                return user;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(GetByIdAsync)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(GetByIdAsync)} in {GetType().Name}");
            }
        }

        public async Task<IEnumerable<XIdentityUser>> GetAllAsync(string? username = null, int skip = 0, int take = 10)
        {
            _logger.Information($"Start of {nameof(GetAllAsync)} {nameof(User)} in {GetType().Name}, {(username != null ? $"filtered by {username}," : "")} skip={skip}, take={take}");
            try
            {
                IQueryable<XIdentityUser> users;
                if (username == null)
                {
                    users = _context.Users.Skip(skip).Take(take);
                }
                else
                {
                    users = _context.Users.Where(x => x.NormalizedUserName.Contains(username.ToUpper())).Skip(skip).Take(take);
                }
                _logger.Information($"Return {users.Count()} {nameof(User)}s");
                return users;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(GetAllAsync)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(GetAllAsync)} in {GetType().Name}");
            }
        }

        public async Task<string> GetCurrentUserNameAsync(string userId)
        {
            _logger.Information($"Start of {nameof(GetCurrentUserNameAsync)} in {GetType().Name} with userId {userId}");
            try
            {
                var user = await GetByIdAsync(userId);
                if (user == null)
                {
                    _logger.Error($"{nameof(User)} with id {userId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }
                Log.Information($"Return {nameof(XIdentityUser.UserName)} with id {user.Id}");
                return user.UserName;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(GetCurrentUserNameAsync)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(GetCurrentUserNameAsync)} in {GetType().Name}");
            }
        }

        public async Task<XIdentityUser> UpdateAsync(string userId, UpdateUserDto updateDto)
        {
            _logger.Information($"Start of {nameof(UpdateAsync)} in {GetType().Name} with userId {userId}");
            try
            {
                var user = await GetByIdAsync(userId);
                if (user == null)
                {
                    _logger.Error($"{nameof(User)} with id {updateDto} is not found");
                    throw Exceptions.EntityNotFound<XIdentityUser>();
                }
                user.Status = updateDto.Status ?? user.Status;
                user.Telegram = updateDto.Telegram ?? user.Telegram;
                if (updateDto.Image != null)
                {
                    var image = await _context.ContentObjects.FirstOrDefaultAsync(x =>
                        x.Id == updateDto.Image && x.Type == NContentObject.ContentType.Image);
                    user.Image = image ?? user.Image;
                }

                _context.Update(user);
                await _context.SaveChangesAsync();
                _logger.Information($"Return updated {nameof(User)} with id {user.Id}");
                return user;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(UpdateAsync)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(UpdateAsync)} in {GetType().Name}");
            }
        }

        public async Task<string> UpdateRootAsync(string userId, UpdateUserCredDto updateDto)
        {
            _logger.Information($"Start of {nameof(UpdateRootAsync)} in {GetType().Name} with userId {userId}");
            try
            {
                var user = await GetByIdAsync(userId);
                if (user == null)
                {
                    _logger.Error($"{nameof(User)} with id {userId} is not found");
                    throw Exceptions.EntityNotFound<User>();
                }
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, updateDto.Email);
                var id = await user.SendConfirmationEmail(_context, updateDto, code);
                Log.Information($"Return {nameof(User)} with id {user.Id}");
                return id;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(UpdateRootAsync)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(UpdateRootAsync)} in {GetType().Name}");
            }
        }

        public async Task<string> ConfirmRootAsync(string userId, string id, string code)
        {
            _logger.Information($"Start of {nameof(ConfirmRootAsync)} in {GetType().Name} with userId {userId}");
            try
            {

                var confirmation = await _context.UserConfirmations.FindAsync(id);

                if (confirmation == null)
                {
                    _logger.Error($"{nameof(XIdentityUserConfirm)} with id {id} is not found");
                    throw Exceptions.EntityNotFound<XIdentityUserConfirm>() ;
                }

                var result = await confirmation.TryToConfirm(code);
                if (result)
                {
                    var user = await GetByIdAsync(userId);
                    var confUser = await GetByIdAsync(confirmation.UserId);
                    if (user == null || confUser == null)
                    {
                        _logger.Error($"{nameof(User)} with id {userId} is not found");
                        throw Exceptions.EntityNotFound<User>();
                    }

                    if (userId != confirmation.UserId || user?.Id != confUser?.Id)
                    {
                        var mes = "Wrong user for conformation";
                        _logger.Error(mes);
                        return mes;
                    }

                    if (confirmation.Email != null)
                    {
                        await _userManager.ChangeEmailAsync(user, confirmation.Email, confirmation.MailCode);
                    }

                    if (confirmation.UserName != null)
                    {
                        await _userManager.SetUserNameAsync(user, confirmation.UserName);
                    }

                    _context.UserConfirmations.Remove(confirmation);
                    await _context.SaveChangesAsync();

                    Log.Information($"Return result code or empty");
                    return "";
                }
                var res = "Wrong code";
                _logger.Error(res);
                return res;
            }
            catch (Exception e)
            {
                if (e is not BaseSbeuException)
                {
                    _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(ConfirmRootAsync)}");
                    _logger.Fatal(e.Message);
                    _logger.Fatal(e?.InnerException?.Message ?? "");
                }
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(ConfirmRootAsync)} in {GetType().Name}");
            }
        }

        public async Task<IEnumerable<NContentObject>> GetContentsAsync(string userId)
        {
            _logger.Information($"Start of {nameof(GetContentsAsync)} in {GetType().Name} with userId {userId}");
            try
            {
                var user = await GetByIdAsync(userId);
                var contents = user.Contents.OrderBy(x =>
                    x.UsersPosition.First(s => s.UserId == userId && s.ContentId == x.Id).Position);
                return contents;
            }
            catch (Exception e)
            {
                _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(GetContentsAsync)}");
                _logger.Fatal(e.Message);
                _logger.Fatal(e?.InnerException?.Message ?? "");
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(GetContentsAsync)} in {GetType().Name}");
            }
        }

        public async Task MoveContent(string id, uint position, string userId)
        {
            _logger.Information($"Start of {nameof(MoveContent)} in {GetType().Name} with id {id}, new position {position} for user {userId}");
            try
            {
                var user = await GetByIdAsync(userId);
                var content = user.ContentsPosition.FirstOrDefault(x => x.ContentId == id);
                if (content == null)
                {
                    _logger.Error($"Content with id {id} is not found");
                    throw Exceptions.EntityNotFound<NContentObject>();
                }
                var old = content.Position;
                if (old == position)
                {
                    return;
                }
                if (old > position)
                {
                    user.ContentsPosition.OrderBy(x => x.Position).ToArray().ToList().ForEach( x =>
                    {
                        if (x.Position >= position && x.Position < old)
                        {
                            x.Position++;
                            _context.Update(x);
                            
                        }else
                        if (x.Position == old)
                        {
                            x.Position = position;
                            _context.Update(x);
                        }
                    });
                }
                else
                {
                    user.ContentsPosition.OrderByDescending(x=>x.Position).ToArray().ToList().ForEach(x =>
                    {
                        if (x.Position <= position && x.Position > old)
                        {
                            x.Position--;
                            _context.Update(x);
                        }else
                        if (x.Position == old)
                        {
                            x.Position = position;
                            _context.Update(x);
                        }
                    });
                }

                
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.Fatal($"Something went wrong in {GetType().Name} in {nameof(MoveContent)}");
                _logger.Fatal(e.Message);
                _logger.Fatal(e?.InnerException?.Message ?? "");
                throw;
            }
            finally
            {
                _logger.Information($"End of {nameof(MoveContent)} in {GetType().Name}");
            }
        }
    }
}
