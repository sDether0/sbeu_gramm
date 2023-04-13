using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.DataLayer.DataBase.Entities
{
    public class Tags
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong? Id { get; set; }
        public string Tag { get; set; }
        public ulong Popularity { get; set; }
        public virtual ICollection<NPost> Posts { get; set; }
    }
}
