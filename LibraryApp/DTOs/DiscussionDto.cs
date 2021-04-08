using LibraryApp.DTOs.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DTOs
{
    public class DiscussionDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public UserDto User { get; set; }
        public AssetDto Asset { get; set; }
    }
}
