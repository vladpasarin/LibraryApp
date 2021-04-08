using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DTOs
{
    public class ReadingLogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int NumberOfPages { get; set; }
        public int TimeMinutes { get; set; }
        public string ShortNote { get; set; }
        public DateTime DateLogged { get; set; }
        public UserDto User { get; set; }
        public int UserId { get; set; }
    }
}
