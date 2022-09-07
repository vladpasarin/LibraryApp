namespace LibraryApp.Entities
{
    public class ChallengeType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public Challenge Challenge { get; set; }
    }
}
