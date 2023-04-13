using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Models.Responses
{
    public class SmallContentDto : BaseDto
    {
        public string Link { get; set; }
        public ContentType Type { get; set; }
    }
}
