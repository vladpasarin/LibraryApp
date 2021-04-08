namespace LibraryApp.Entities
{
    public class ReplyTo
    {
        public int Id { get; set; }
        public Comment Comment { get; set; }
        public Comment Response { get; set; }
    }
}
