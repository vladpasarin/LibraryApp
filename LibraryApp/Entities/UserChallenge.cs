namespace LibraryApp.Entities
{
    public class UserChallenge
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ChallengeId { get; set; }
        public Challenge Challenge { get; set; }
        public int Progress { get; set; }
    }
}
