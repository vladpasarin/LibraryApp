using LibraryApp.DTOs;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _repo;

        public QuoteService(IQuoteRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> CreateAsync(QuoteDto quoteDto)
        {
            return await _repo.CreateAsync(quoteDto);
        }

        public async Task<List<QuoteDto>> GetBookQuotes(int bookId)
        {
            return await _repo.GetBookQuotes(bookId);
        }

        public async Task<QuoteDto> GetById(int id)
        {
            return await _repo.GetById(id);
        }
    }
}
