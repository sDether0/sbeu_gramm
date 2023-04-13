using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Models.Responses
{
    public class NotificationUserDto : SmallUserDto
    {
        public bool IsFollow { get; set; }
    }
}
