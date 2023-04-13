using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces
{
    internal interface IWatchable : IBaseEntity
    {
        public ICollection<XIdentityUser> WatchedUsers { get; set; }
    }
}
