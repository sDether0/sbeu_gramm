using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.DataLayer.DataBase.Entities.Relate
{
    public class XIdentityUserContent
    {
        public virtual XIdentityUser User { get; set; }
        public virtual NContentObject Content { get; set; }
        public string UserId { get; set; }
        public string ContentId { get; set; }

        public uint Position { get; set; }
    }
}
