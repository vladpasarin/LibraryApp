using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNr { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int LibraryCardId { get; set; }  
        public string PasswordResetToken { get; set; } = string.Empty;
        public LibraryCard LibraryCard { get; set; }
        public List<Bookmark> Bookmarks { get; set; }
        public List<Rating> UserRatings { get; set; }
    }
}
