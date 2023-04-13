using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Models.Interfaces
{
    public interface IIsLike
    {
        public string Id { get; set; }
        public bool IsLiked { get; set; }
    }
}
