using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ClientURI { get; set; }
    }
}
