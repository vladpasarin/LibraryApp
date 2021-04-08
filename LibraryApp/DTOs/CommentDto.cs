using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public UserDto User { get; set; }
        public int UserId { get; set; }
        public DiscussionDto Discussion { get; set; }
        public int DiscussionId { get; set; }
    }
}
