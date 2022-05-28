using LibraryApp.Entities.Assets.Tags;
using System;
using System.Collections.Generic;

namespace LibraryApp.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public Double Cost { get; set; }
        public string ImageUrl { get; set; }
        public int AvailabilityStatusId { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
        public string Type { get; set; }
        public int NrOfCopies { get; set; }
        public int NrOfAvailableCopies { get; set; }
        public List<AssetTag> AssetTags { get; set; }
        public List<Bookmark> Bookmarks { get; set; }
        public List<Rating> AssetRatings { get; set; }
    }
}
