namespace LibraryApp.DTOs
{
    public class UserChallengeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int ChallengeId { get; set; }
        public ChallengeDto Challenge { get; set; }
    }
}
