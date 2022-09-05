using LibraryApp.DTOs;
using LibraryApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IQuoteRepository : IGenericRepository<Quote>
    {
        public Task<bool> CreateAsync(QuoteDto quoteDto);
        public Task<List<QuoteDto>> GetBookQuotes(int bookId);
        public Task<QuoteDto> GetById(int id);
        public Task<List<QuoteDto>> GetUserQuotes(int userId);
    }
}
