using SBEU.Gramm.Models.Responses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Models.Requests.Create
{
    public class CreateStoryDto
    {
        public string? Description { get; set; }
        public string Content { get; set; }
        public string Sound { get; set; }
        public List<SmallUserDto> Tagged { get; set; }
    }
}
