using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase.Entities;

namespace SBEU.Gramm.Middleware.Repositories.Interfaces
{
    public interface INLikeRepository : IBaseRepository<NLike>
    {
        /// <summary>
        /// > Get all the likes for a given contentId
        /// </summary>
        /// <param name="contentId">The id of the content that you want to get the likes for.</param>
        /// <returns>
        /// A list of NLike objects.
        /// </returns>
        Task<IEnumerable<NLike>> GetByContentId(string contentId);
        /// <summary>
        /// > Create a new like for a given contentId and userId
        /// </summary>
        /// <param name="userId">The user who liked the content</param>
        /// <param name="contentId">The id of the content that is being liked.</param>
        /// <returns>
        /// A like object
        /// </returns>
        Task<NLike> Create(string userId, string contentId);
        /// <summary>
        /// > Delete a like from a content
        /// </summary>
        /// <param name="userId">The user who liked the content</param>
        /// <param name="contentId">The id of the content that the user liked</param>
        /// <returns>
        /// A boolean value.
        /// </returns>
        Task<bool> Delete(string userId, string contentId);
    }
}
