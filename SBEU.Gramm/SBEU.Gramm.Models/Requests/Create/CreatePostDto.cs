using SBEU.Gramm.Models.Responses;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Models.Requests.Create
{
    public class CreatePostDto
    {
        public List<string> Tags { get; set; }
        [Required,MinLength(1)]
        public List<string> Content { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public List<SmallUserDto> Tagged { get; set; }
    }
}
