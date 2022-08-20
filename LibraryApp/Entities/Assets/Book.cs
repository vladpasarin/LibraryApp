using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public int AssetId { get; set; }
        public int PublicationYear { get; set; }
        public string Edition { get; set; }
        public string Publisher { get; set; }
        public string DeweyIndex { get; set; }
        public string Language { get; set; }
        public string Summary { get; set; }
        public Asset Asset { get; set; }
        public List<Quote> BookQuotes { get; set; }
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
    }
}
