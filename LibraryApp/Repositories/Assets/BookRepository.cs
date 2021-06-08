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
        private readonly DbSet<EBook> _eBooksTable;
        private readonly DbSet<AudioBook> _audioBooksTable;

        public BookRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _eBooksTable = context.Set<EBook>();
            _audioBooksTable = context.Set<AudioBook>();
        }

        public async Task<BookDto> Get(int id)
        {
            var book = await _context.Books
                .Include(x => x.Asset)
                .FirstAsync(x => x.Id == id);
            return _mapper.Map<BookDto>(book);
        }

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
        }

        public async Task<bool> AddEBook(EBookDto newEBook)
        {
            var eBook = _mapper.Map<EBook>(newEBook);
            await _eBooksTable.AddAsync(eBook);
            return await SaveChanges();
        }
        
        public async Task<bool> AddAudioBook(AudioBookDto newAudioBook)
        {
            var audioBook = _mapper.Map<AudioBook>(newAudioBook);
            await _audioBooksTable.AddAsync(audioBook);
            return await SaveChanges();
        }

        public void DeleteEBook(EBookDto eBook)
        {
            var deleted = _mapper.Map<EBook>(eBook);
            _eBooksTable.Remove(deleted);
        }

        public void DeleteAudioBook(AudioBookDto audioBookDto)
        {
            var deleted = _mapper.Map<AudioBook>(audioBookDto);
            _audioBooksTable.Remove(deleted);
        }

        public async Task<List<EBook>> GetAllEBooks()
        {
            return await _eBooksTable.ToListAsync();
        }

        public async Task<List<AudioBook>> GetAllAudioBooks()
        {
            return await _audioBooksTable.ToListAsync();
        }

        public void UpdateEBook(EBook ebook) 
        {
            _eBooksTable.Update(ebook);
        }

        public void UpdateAudioBook(AudioBook audioBook)
        {
            _audioBooksTable.Update(audioBook);
        }
    }
}
