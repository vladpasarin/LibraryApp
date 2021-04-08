using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Entities
{
    public class CheckoutHistory
    {
        public int Id { get; set; }
        [Required]
        public DateTime CheckedOut { get; set; }
        public DateTime? CheckedIn { get; set; }
        [Required]
        public Asset Asset { get; set; }
        [Required]
        public LibraryCard LibraryCard { get; set; }
    }
}
