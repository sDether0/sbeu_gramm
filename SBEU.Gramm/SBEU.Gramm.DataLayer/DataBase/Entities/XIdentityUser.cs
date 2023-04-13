
using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations.Schema;
using SBEU.Gramm.DataLayer.DataBase.Entities.Base;
using SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces;
using SBEU.Gramm.DataLayer.DataBase.Entities.Relate;

namespace SBEU.Gramm.DataLayer.DataBase.Entities
{
    public class XIdentityUser : IdentityUser, IBaseEntity
    {
        public string? Status { get; set; }
        public string? Telegram { get; set; }
        public virtual NContentObject? Image { get; set; }
        public virtual ICollection<NPost> Posts { get; set; }
        public virtual ICollection<NStory> Stories { get; set; }
        public virtual ICollection<XIdentityUser> Following { get; set; }
        public virtual ICollection<XIdentityUser> Followers { get; set; }
        public virtual ICollection<NStory> STaggedIn { get; set; }
        public virtual ICollection<NStory> WatchedStories { get; set; }
        public virtual ICollection<NPost> WatchedPosts { get; set; }
        public virtual ICollection<NPost> PTaggedIn { get; set; }
        public virtual ICollection<NNotification> Notifications { get; set; }
        public virtual ICollection<NContentObject> Contents { get; set; }
        public virtual ICollection<XIdentityUserContent> ContentsPosition { get; set; }

        public virtual SubscriptionPlan SubscriptionPlan { get; set; }
    }

    public class User : BaseEntity { }
}
