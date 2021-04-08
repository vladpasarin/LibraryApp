using LibraryApp.DTOs.Assets;
using System;

namespace LibraryApp.DTOs
{
    public class CheckoutDto
    {
        public int Id { get; set; }
        public DateTime CheckedOutSince { get; set; }
        public DateTime CheckedOutUntil { get; set; }
        public AssetDto Asset { get; set; }
        public LibraryCardDto LibraryCard { get; set; }
    }
}
