using System;

namespace LibraryApp.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Seen { get; set; } = false;
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
