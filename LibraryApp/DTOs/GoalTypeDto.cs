using System.Collections.Generic;

namespace LibraryApp.DTOs
{
    public class GoalTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GoalDto> Goals { get; set; }
    }
}
