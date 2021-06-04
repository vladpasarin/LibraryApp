using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs.Assets;
using LibraryApp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Entities;

namespace LibraryApp.Repositories.Assets
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly IMapper _mapper;

        public BookRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<BookDto> Get(int id)
        {
            var book = await _context.Books
                .Include(x => x.Asset)
                .FirstAsync(x => x.Id == id);
            return _mapper.Map<BookDto>(book);
        }

        /*
        public async Task<EBookDto> GetEBook(int id)
        {
            var eBook = await _context.EBooks
                .Include(x => x.Asset)
                .FirstAsync(x => x.Id == id);
            return _mapper.Map<EBookDto>(eBook);
        }

        public async Task<AudioBookDto> GetAudioBook(int id)
        {
            var audioBook = await _context.AudioBooks
                .Include(x => x.Asset)
                .FirstAsync(x => x.Id == id);
            return _mapper.Map<AudioBookDto>(audioBook); 
        }*/
    }
}
