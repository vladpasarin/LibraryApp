using System.Collections.Generic;

namespace LibraryApp.Entities.Assets.Tags
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AssetTag> AssetTags { get; set; }
    }
}
