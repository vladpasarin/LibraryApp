using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DTOs
{
    public class ChallengeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; } = false;
        public bool Started { get; set; } = false;
        public int Threshold { get; set; }
        public List<UserDto> UserChallenge { get; set; }
    }
}
