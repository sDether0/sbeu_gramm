using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase.Entities;

namespace SBEU.Gramm.Middleware.Repositories.Interfaces
{
    public interface INPostRepository : IBaseRepository<NPost>
    {
        /// <summary>
        /// It takes a post and a userId, finds the user with that id, sets the post's author to that
        /// user, and then creates the post
        /// </summary>
        /// <param name="NPost">The model that we're creating</param>
        /// <param name="userId">The id of the user who is creating the post.</param>
        /// <returns>
        /// The post is being returned.
        /// </returns>
        Task<NPost> Create(NPost post, string userId);
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
        Task<IEnumerable<NPost>> GetAll(string UserId,int skip = 0, int take = 10);
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
        Task<IEnumerable<NPost>> GetRadius(string UserId, double lat, double lng);
        /// <summary>
        /// It adds a user to the list of users who have watched a story
        /// </summary>
        /// <param name="id">The id of the story to be watched</param>
        /// <param name="userId">The user who is watching the story</param>
        /// <returns>
        /// A boolean value.
        /// </returns>
        Task<bool> Watch(string id, string userId);
    }
}
