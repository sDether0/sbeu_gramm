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
    public class NLike : DeletableEntity
    {
        public virtual XIdentityUser Author { get; set; }
        public virtual NPost? Post { get; set; }
        public virtual NCommentary? Commentary { get; set; }
        public virtual NStory? Story { get; set; }
        public virtual NContentObject? Content { get; set; }

    }
}
