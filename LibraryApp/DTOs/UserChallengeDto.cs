using System;

namespace LibraryApp.DTOs
{
    public class UserChallengeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int ChallengeId { get; set; }
        public ChallengeDto Challenge { get; set; }
        public int Progress { get; set; }
        public int Threshold { get; set; }
        public bool Completed { get; set; } = false;
        public DateTime? DateStarted { get; set; } = null;
        public DateTime? DateCompleted { get; set; } = null;
    }
}
