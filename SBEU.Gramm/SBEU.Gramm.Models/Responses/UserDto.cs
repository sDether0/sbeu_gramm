using System.Text.Json.Serialization;

namespace SBEU.Gramm.Models.Responses
{
    public class UserDto : BaseDto
    {
        public string NickName { get; set; }
        public string? Status { get; set; }
        public string? Telegram { get; set; }
        public SmallContentDto? Image { get; set; }
        public List<SmallUserDto> Followers { get; set; }
        public List<SmallUserDto> Following { get; set; }
        public List<SmallPostDto> Posts { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsFollow { get; set; }
    }
}
