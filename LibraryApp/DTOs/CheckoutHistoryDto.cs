using LibraryApp.DTOs.Assets;
using System;

namespace LibraryApp.DTOs
{
    public class CheckoutHistoryDto
    {
        public int Id { get; set; }
        public DateTime CheckedOut { get; set; }
        public DateTime? CheckedIn { get; set; }
        public AssetDto Asset { get; set; }
        public LibraryCardDto LibraryCard { get; set; }
    }
}
