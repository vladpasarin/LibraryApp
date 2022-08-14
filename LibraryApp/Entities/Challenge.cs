using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Entities
{
    public class Challenge
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Completed { get; set; } = false;
        public bool Started { get; set; } = false;
        public int Threshold { get; set; }
        public DateTime? DateStarted { get; set; } = DateTime.MinValue;
        public DateTime? DateCompleted { get; set; } = DateTime.MaxValue;
        public List<UserChallenge> UserChallenges { get; set; }
    }
}
