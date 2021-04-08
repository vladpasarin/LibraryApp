using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DTOs
{
    public class BadgeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateAcquired { get; set; }
        public ChallengeDto Challenge { get; set; }
        public int ChallengeId { get; set; }
    }
}
