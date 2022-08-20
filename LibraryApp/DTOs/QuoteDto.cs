using LibraryApp.DTOs.Assets;

namespace LibraryApp.DTOs
{
    public class QuoteDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public UserDto User { get; set; }
        public int UserId { get; set; }
        public BookDto Book { get; set; }
        public int BookId { get; set; }
    }
}
