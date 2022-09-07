using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DTOs.Assets
{
    public class GenericBookDto
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string ASIN { get; set; }
        public int PublicationYear { get; set; }
        public string Edition { get; set; }
        public string Publisher { get; set; }
        public string DeweyIndex { get; set; }
        public string Language { get; set; }
        public string Summary { get; set; }
        public AssetDto Asset { get; set; }
        // Audio Book
        public int LengthMinutes { get; set; }
        // EBook
        public Double Size { get; set; }
        public IEnumerable<TagDto> Tags { get; set; }
    }
}
