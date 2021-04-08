using System;

namespace LibraryApp.DTOs.Assets
{
    public class EBookDto
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ASIN { get; set; }
        public int PublicationYear { get; set; }
        public Double Size { get; set; }
        public string Edition { get; set; }
        public string Publisher { get; set; }
        public string DeweyIndex { get; set; }
        public string Language { get; set; }
        public string Summary { get; set; }
        public AssetDto Asset { get; set; }
    }
}
