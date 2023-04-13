using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.DataLayer.DataBase.Entities.Relate
{
    [Table("NContentObjectNPost")]
    public class NPostContent
    {
        public string ContentsId { get; set; }
        public string PostsId { get; set; }
        public ushort Position { get; set; }
    }
}
