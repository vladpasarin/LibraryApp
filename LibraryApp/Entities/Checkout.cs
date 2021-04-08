using System;

namespace LibraryApp.Entities
{
    public class Checkout
    {
        public int Id { get; set; }
        public DateTime CheckedOutSince { get; set; }
        public DateTime CheckedOutUntil { get; set; }
        public Asset Asset { get; set; }
        public LibraryCard LibraryCard { get; set; }
    }
}
