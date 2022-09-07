using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Users
{
    public class QuoteRepository : GenericRepository<Quote>, IQuoteRepository
    {
        private readonly IMapper _mapper;

        public QuoteRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(QuoteDto quoteDto)
        {
            var quote = _mapper.Map<Quote>(quoteDto);
            await Create(quote);
            return await SaveChanges();
        }

        public async Task<List<QuoteDto>> GetBookQuotes(int bookId)
        {
            var quotes = await _context.Quotes
                .Where(q => q.BookId == bookId)
                .ToListAsync();

            return _mapper.Map<List<QuoteDto>>(quotes);
        }

        public async Task<QuoteDto> GetById(int id)
        {
            return _mapper.Map<QuoteDto>(await FindById(id));
        }

        public async Task<List<QuoteDto>> GetUserQuotes(int userId)
        {
            var userQuotes = await _context.Quotes
                .Where(q => q.UserId == userId)
                .Include(q => q.Book)
                .ToListAsync();

            return _mapper.Map<List<QuoteDto>>(userQuotes);
        }
    }
}
