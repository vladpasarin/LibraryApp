using System;
using System.Collections.Generic;

namespace LibraryApp.DTOs
{
    public class LibraryCardDto
    {
        public int Id { get; set; }
        public Double CurrentFees { get; set; }
        public DateTime DateIssued { get; set; }
        public virtual IEnumerable<CheckoutDto> Checkouts { get; set; }
    }
}
