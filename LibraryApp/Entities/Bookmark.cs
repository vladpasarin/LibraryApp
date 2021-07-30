using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Entities
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
