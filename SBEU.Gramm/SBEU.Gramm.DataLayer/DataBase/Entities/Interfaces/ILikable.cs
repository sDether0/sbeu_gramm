using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces
{
    public interface ILikable : IBaseEntity
    {
        public ICollection<NLike> Likes { get; set; }
        //[NotMapped]
        //public bool IsLiked { get; set; }
    }
}
