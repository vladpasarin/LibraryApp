using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DTOs
{
    public class ReplyToDto
    {
        public int Id { get; set; }
        public CommentDto Comment { get; set; }
        public int CommentId { get; set; }
        public CommentDto Response { get; set; }
        public int ResponseId { get; set; }
    }
}
