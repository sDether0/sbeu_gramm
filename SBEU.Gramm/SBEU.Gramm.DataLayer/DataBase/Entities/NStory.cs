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
    public class NStory : DeletableEntity, ILikable, IWithUTagEntity, IWatchable
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual NContentObject Content { get; set; }
        public virtual NContentObject? Sound { get; private set; }
        public virtual XIdentityUser Author { get; set; }
        public virtual ICollection<NLike> Likes { get; set; }
        public virtual ICollection<XIdentityUser> TaggedUsers { get; set; }
        public virtual ICollection<XIdentityUser> WatchedUsers { get; set; }
        
        
        /// <summary>
        /// This function sets the sound of the object to the sound passed in
        /// </summary>
        /// <param name="NContentObject">This is the content object that you want to set.</param>
        public void SetSound(NContentObject content)
        {
            if (content.Type != NContentObject.ContentType.Sound) throw new Exception("Content is not sound");
            Sound = content;
        }
    }
}
