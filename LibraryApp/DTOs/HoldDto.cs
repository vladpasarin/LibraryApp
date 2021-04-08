using LibraryApp.DTOs.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DTOs
{
    public class HoldDto
    {
        public int Id { get; set; }
        public DateTime HoldPlaced { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime HoldValidUntil { get; set; }
        public virtual AssetDto Asset { get; set; }
        public virtual LibraryCardDto LibraryCard { get; set; }
    }
}
