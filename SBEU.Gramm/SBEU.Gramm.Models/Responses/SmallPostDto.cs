using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.Models.Interfaces;

namespace SBEU.Gramm.Models.Responses
{
    public class SmallPostDto : BaseDto, IIsLike
    {
        public SmallUserDto Author { get; set; }
        public List<SmallContentDto> Content { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CommentariesCount { get; set; }
        public int LikesCount { get; set; }
        public int ViewsCount { get; set; }
        public bool IsLiked { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
