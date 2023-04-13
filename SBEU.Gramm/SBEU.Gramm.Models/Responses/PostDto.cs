using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.Models.Interfaces;

namespace SBEU.Gramm.Models.Responses
{
    public class PostDto : BaseDto, IIsLike
    {
        public SmallUserDto Author { get; set; }
        public List<string> Tags { get; set; }
        public List<SmallContentDto> Content { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CommentaryDto> Commentaries { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LikesCount { get; set; }
        public List<SmallUserDto> Tagged { get; set; }
        public bool IsLiked { get; set; }
        public int ViewsCount { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
