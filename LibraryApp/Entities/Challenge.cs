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
        public int Threshold { get; set; } = 0;
        public bool Started { get; set; } = false;
        public List<UserChallenge> UserChallenges { get; set; }  
    }
}
