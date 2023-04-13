using SBEU.Gramm.DataLayer.DataBase.Entities;

namespace SBEU.Gramm.Middleware.Repositories.Interfaces;

public interface INCommentaryRepository : IBaseRepository<NCommentary>
{
    Task<IEnumerable<NCommentary>> GetByPost(string postid);
    Task<NCommentary> Create(NCommentary comment, string userId);
}