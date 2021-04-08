using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Entities
{
    public class Discussion
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Description { get; set; }
        public User User { get; set; }
        public Asset Asset { get; set; }
    }
}
