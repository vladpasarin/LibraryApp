using System;

namespace LibraryApp.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNr { get; set; }
        public Double OverdueFees { get; set; }
        public LibraryCardDto  LibraryCard { get; set; }
        public int LibraryCardId { get; set; }
    }
}
