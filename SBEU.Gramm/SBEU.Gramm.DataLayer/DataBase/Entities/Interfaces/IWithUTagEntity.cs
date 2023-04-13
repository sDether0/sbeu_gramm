using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces
{
    public interface IWithUTagEntity : IBaseEntity
    {
        public ICollection<XIdentityUser> TaggedUsers { get; set; }
    }
}
