using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using SBEU.Gramm.Models.Interfaces;

namespace SBEU.Gramm.Models.Responses
{
    public class ContentDto : BaseDto, IIsLike
    {
        public string Link { get; set; }
        public int LikesCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public ContentType Type { get; set; }
        public bool IsLiked { get; set; }
        
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContentType
    {
        Image,
        Video,
        Sound
    }
}
