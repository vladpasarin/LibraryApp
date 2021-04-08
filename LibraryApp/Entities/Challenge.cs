using System;
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
        public bool Completed { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public User User { get; set; }
    }
}
