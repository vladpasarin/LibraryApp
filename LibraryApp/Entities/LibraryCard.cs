using System;
using System.Collections.Generic;

namespace LibraryApp.Entities
{
    public class LibraryCard
    {
        public int Id { get; set; }
        public Double CurrentFees { get; set; }
        public DateTime DateIssued { get; set; }
        public bool MaxCheckout { get; set; }
        public virtual IEnumerable<Checkout> Checkouts { get; set; }
    }
}
