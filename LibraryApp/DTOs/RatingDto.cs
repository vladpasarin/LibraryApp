using LibraryApp.DTOs.Assets;

namespace LibraryApp.DTOs
{
    public class RatingDto
    {
        public int Score { get; set; }
        public string Review { get; set; }
        public int UserId { get; set; }
        public  UserDto User { get; set; }
        public int AssetId { get; set; }
        public AssetDto Asset { get; set; }
    }
}
