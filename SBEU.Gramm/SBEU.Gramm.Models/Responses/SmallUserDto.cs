using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Models.Responses
{
    public class SmallUserDto : BaseDto
    {
        public string NickName { get; set; }
        public string? Image { get; set; }
    }
}
