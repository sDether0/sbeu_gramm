using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase.Entities;

namespace SBEU.Gramm.Middleware.Repositories.Interfaces
{
    public interface INStoryRepository : IBaseRepository<NStory>
    {
        Task<NStory> Create(NStory story, string userId);
        Task<bool> Watch(string id, string userId);
        Task<IEnumerable<NStory>> GetAll(string userId);
    }
}
