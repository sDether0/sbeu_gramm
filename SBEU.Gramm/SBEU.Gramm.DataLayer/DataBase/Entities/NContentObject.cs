using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SBEU.Gramm.DataLayer.DataBase.Entities.Base;
using SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces;
using SBEU.Gramm.DataLayer.DataBase.Entities.Relate;

namespace SBEU.Gramm.DataLayer.DataBase.Entities
{
    public class NContentObject : DeletableEntity, ILikable
    {
        public virtual ICollection<NLike> Likes { get; set; }
        public string PhysicalPath { get; set; }
        public string Link { get; set; }
        public virtual ICollection<NPost> Posts { get; set; }
        public DateTime CreatedAt { get; set; }
        public ContentType Type { get; set; }

        public virtual ICollection<NPostContent> PostPositions { get; set; }
        public virtual ICollection<XIdentityUser> Users { get; set; }
        public virtual ICollection<XIdentityUserContent> UsersPosition { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ContentType
        {
            Image,
            Video,
            Sound
        }
    }
}
