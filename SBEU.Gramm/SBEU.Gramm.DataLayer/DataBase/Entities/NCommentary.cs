using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase.Entities.Base;
using SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces;

namespace SBEU.Gramm.DataLayer.DataBase.Entities
{
    public class NCommentary : DeletableEntity, ILikable
    {
        public string Text { get; set; }
        public virtual XIdentityUser Author { get; set; }
        public virtual NPost Post { get; set; }
        public virtual ICollection<NLike> Likes { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
