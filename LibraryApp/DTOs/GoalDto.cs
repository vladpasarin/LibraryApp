using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DTOs
{
    public class GoalDto
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public int Progress { get; set; } = 0;
        [Required]
        public int Target { get; set; }
        public UserDto User { get; set; }
        public int UserId { get; set; }
        public GoalTypeDto GoalType { get; set; }
        public int GoalTypeId { get; set; }
    }
}
