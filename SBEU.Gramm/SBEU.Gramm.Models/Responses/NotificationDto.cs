using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Models.Responses
{
    public class NotificationDto
    {
        public DateTime CreateDateTime { get; set; }
        public virtual SmallUserDto Target { get; set; }
        public virtual NotificationUserDto Author { get; set; }
        public virtual SmallPostDto? Post { get; set; }
        public virtual CommentaryDto? Commentary { get; set; }
        public virtual StoryDto? Story { get; set; }
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
