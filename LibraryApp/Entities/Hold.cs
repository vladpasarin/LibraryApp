using System;

namespace LibraryApp.Entities
{
    public class Hold
    {
        public int Id { get; set; }
        public DateTime HoldPlaced { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime HoldValidUntil { get; set; }
        public virtual Asset Asset { get; set; }
        public virtual LibraryCard LibraryCard { get; set; }
    }
}
