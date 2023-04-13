using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase.Entities.Base;

namespace SBEU.Gramm.DataLayer.DataBase.Entities
{
    public class NNotification : BaseEntity
    {
        public DateTime CreateDateTime { get; set; }
        public virtual XIdentityUser Target { get; set; }
        public virtual XIdentityUser Author { get; set; }
        public virtual NPost? Post { get; set; }
        public virtual NCommentary? Commentary { get; set; }
        public virtual NStory? Story { get; set; }
        public NotificationType NotificationType { get; set; }
    }

    public enum NotificationType 
    {
        Subscribe,
        TagInPost,
        TagInStory,
        CommentYourPost,
        LikePost,
        LikeStory,
        LikeComment
    }
}
