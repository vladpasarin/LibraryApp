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
        public int Threshold { get; set; } = 0;
        public bool Started { get; set; } = false;
        public List<UserChallengeDto> UserChallenges { get; set; }
    }
}
