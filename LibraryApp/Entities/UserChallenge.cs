using System;

namespace LibraryApp.Entities
{
    public class UserChallenge
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ChallengeId { get; set; }
        public Challenge Challenge { get; set; }
        public int Progress { get; set; } = 0;
        public int Threshold { get; set; } = 0;
        public bool Completed { get; set; } = false;
        public DateTime? DateStarted { get; set; } = null;
        public DateTime? DateCompleted { get; set; } = null;
    }
}
