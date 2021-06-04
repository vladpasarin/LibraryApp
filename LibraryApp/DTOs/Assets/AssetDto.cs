using System;

namespace LibraryApp.DTOs.Assets
{
    public class AssetDto
    {
        public int Id { get; set; }
        public StatusDto Status { get; set; }
        public Double Cost { get; set; }
        public string ImageUrl { get; set; }
        public AssetTypeDto AssetType { get; set; }
    }
}
