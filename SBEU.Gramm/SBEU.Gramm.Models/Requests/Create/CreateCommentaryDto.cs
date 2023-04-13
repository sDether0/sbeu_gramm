using SBEU.Gramm.Models.Responses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Models.Requests.Create
{
    public class CreateCommentaryDto
    {
        public string Post { get; set; }
        public string Text { get; set; }
    }
}
