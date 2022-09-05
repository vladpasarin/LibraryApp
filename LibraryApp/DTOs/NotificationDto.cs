using System;

namespace LibraryApp.DTOs
{
    public class NotificationDto
    {
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Seen { get; set; } = false;
    }
}
