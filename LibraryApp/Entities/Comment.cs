using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public User User { get; set; }
        public Discussion Discussion { get; set; }
    }
}
