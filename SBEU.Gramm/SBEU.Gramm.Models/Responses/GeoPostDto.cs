namespace SBEU.Gramm.Models.Responses
{
    public class GeoPostDto : BaseDto
    {
        public SmallUserDto Author { get; set; }
        public List<SmallContentDto> Content { get; set; }
        public int LikesCount { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}