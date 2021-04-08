namespace LibraryApp.Entities.Assets.Tags
{
    public class AssetTag
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
