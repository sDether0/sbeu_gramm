using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.Models.Interfaces;

namespace SBEU.Gramm.Models.Responses
{
    public class StoryDto : BaseDto, IIsLike
    {
        public string? Description { get; set; }
        public SmallContentDto Content { get; set; }
        public SmallContentDto Sound { get; set; }
        public SmallUserDto Author { get; set; }
        public List<SmallUserDto> Watched { get; set; }
        public List<SmallUserDto> Tagged { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
        public bool IsWatched { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
