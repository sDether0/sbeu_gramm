using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces;

namespace SBEU.Gramm.DataLayer.DataBase.Entities.Base
{
    public class BaseLikeObject : BaseEntity, ILikable
    {
        public ICollection<NLike> Likes { get; set; }
        //public bool IsLiked { get; set; }
    }
}
