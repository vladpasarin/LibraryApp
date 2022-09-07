using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public int Progress { get; set; } = 0;
        public int Target { get; set; } = 0;
        public User User { get; set; }
        public int UserId { get; set; }
        public GoalType GoalType { get; set; }
        public int GoalTypeId { get; set; }
    }
}
