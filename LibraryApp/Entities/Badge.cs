using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Entities
{
    public class Badge
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateAcquired { get; set; }
        public Challenge Challenge { get; set; }
    }
}
