using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Entities
{
    public class ReadingLog
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public int NumberOfPages { get; set; }
        public int TimeMinutes { get; set; }
        public string ShortNote { get; set; }
        public DateTime DateLogged { get; set; }
        public User User { get; set; }
    }
}
