using LibraryApp.DTOs;
using LibraryApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IQuoteService
    {
        Task<bool> CreateAsync(QuoteDto quoteDto);
        Task<List<QuoteDto>> GetBookQuotes(int bookId);
        Task<QuoteDto> GetById(int id);
    }
}
