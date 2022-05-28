namespace LibraryApp.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
