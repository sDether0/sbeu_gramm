using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase.Entities.Base;
using SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces;
using SBEU.Gramm.DataLayer.DataBase.Entities.Relate;

namespace SBEU.Gramm.DataLayer.DataBase.Entities
{
    public class NPost : DeletableEntity, IWithUTagEntity, ILikable, IWatchable
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public virtual XIdentityUser Author { get; set; }
        public virtual ICollection<Tags> Tags { get; set; }
        public virtual ICollection<NContentObject> Contents { get; set; }
        public virtual ICollection<NCommentary> Commentaries { get; set; }
        public virtual ICollection<XIdentityUser> TaggedUsers { get; set; }
        public virtual ICollection<NLike> Likes { get; set; }
        public virtual ICollection<NPostContent> Position { get; set; }
        public virtual ICollection<XIdentityUser> WatchedUsers { get; set; }
    }
}
