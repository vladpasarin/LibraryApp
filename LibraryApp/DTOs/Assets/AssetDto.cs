using System;

namespace LibraryApp.DTOs.Assets
{
    public class AssetDto
    {
        public int Id { get; set; }
        public AvailabilityStatusDto AvailabilityStatus { get; set; }
        public int AvailabilityStatusId { get; set; }
        public Double Cost { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
    }
}
