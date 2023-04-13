using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Models.Requests.Update
{
    public class UpdateUserDto
    {
        public string? Telegram { get; set; }
        public string? Status { get; set; }
        public string? Image { get; set; }
    }
}
