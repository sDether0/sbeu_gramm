using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.Models.Interfaces;

namespace SBEU.Gramm.Models.Responses
{
    public class CommentaryDto : BaseDto, IIsLike
    {
        public string PostId { get; set; }
        public SmallUserDto Author { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
    }
}
