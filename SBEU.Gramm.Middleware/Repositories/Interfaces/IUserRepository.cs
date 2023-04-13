using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.Models.Requests.Update;

namespace SBEU.Gramm.Middleware.Repositories.Interfaces
{
    public interface IUserRepository : IRepository
    {
        Task FollowAsync(string userId, string followId);
        Task UnFollowAsync(string userId, string followId);
        Task<XIdentityUser> GetByIdAsync(string userId);
        Task<IEnumerable<XIdentityUser>> GetAllAsync(string? username = null, int skip=0,int take=10);
        Task<string> GetCurrentUserNameAsync(string userId);
        Task<XIdentityUser> UpdateAsync(string userId,UpdateUserDto updateDto);
        Task<string> UpdateRootAsync(string userId,UpdateUserCredDto updateDto);
        Task<string> ConfirmRootAsync(string userId, string id, string code);
        Task<IEnumerable<NContentObject>> GetContentsAsync(string userId);
        Task MoveContent(string id, uint position, string userId);
    }
}
