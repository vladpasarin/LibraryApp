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
using LibraryApp.DTOs;

namespace LibraryApp.Repositories.Assets
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly IMapper _mapper;
        private readonly DbSet<EBook> _eBooksTable;
        private readonly DbSet<AudioBook> _audioBooksTable;
        private readonly IAssetTagRepository _assetTagRepo;
        private const int MaxAuthors = 5;
        private const int MaxBookSearch = 5;
        private const int AllBooksMax = 20;

        public BookRepository(LibraryDbContext context, IMapper mapper, IAssetTagRepository assetTagRepo) : base(context)
        {
            _mapper = mapper;
            _eBooksTable = context.Set<EBook>();
            _audioBooksTable = context.Set<AudioBook>();
            _assetTagRepo = assetTagRepo;
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
            return await _eBooksTable.Take(AllBooksMax).ToListAsync();
        }

        public async Task<List<AudioBook>> GetAllAudioBooks()
        {
            return await _audioBooksTable.Take(AllBooksMax).ToListAsync();
        }

        public void UpdateEBook(EBook ebook) 
        {
            _eBooksTable.Update(ebook);
        }

        public void UpdateAudioBook(AudioBook audioBook)
        {
            _audioBooksTable.Update(audioBook);
        }

        public async Task<GenericBookDto> GetGenericBook(int assetId)
        {
            var tags = await _assetTagRepo.GetTagsByAssetId(assetId);
            var book = await _context.Books
                .Include(b => b.Asset)
                .FirstAsync(b => b.AssetId == assetId);

            if (book != null)
            {
                var genericBookDto = _mapper.Map<GenericBookDto>(book);
                genericBookDto.Tags = tags;
                return _mapper.Map<GenericBookDto>(genericBookDto);
            }

            var ebook = await _context.EBooks
                .Include(b => b.Asset)
                .FirstAsync(b => b.AssetId == assetId);

            if (ebook != null)
            {
                return _mapper.Map<GenericBookDto>(ebook);
            }

            var audioBook = await _context.AudioBooks
                .Include(b => b.Asset)
                .FirstAsync(b => b.AssetId == assetId);

            if (audioBook != null)
            {
                return _mapper.Map<GenericBookDto>(audioBook);
            }
            else
            {
                return null;
            }
        }

        async Task<List<GenericBookDto>> IBookRepository.GetAllGenericBooks()
        {
            var books = await _context.Books.Take(AllBooksMax).ToListAsync();
            var genericBooks = _mapper.Map<List<GenericBookDto>>(books);
            for (int i = 0; i < books.Count; i++)
            {
                genericBooks[i].Tags = await _assetTagRepo.GetTagsByAssetId(books[i].AssetId);
            }
            return genericBooks;
        }

        public async Task<List<GenericBookDto>> SearchBooksByTitle(string searchValue, string searchType)
        {
            var books = new List<GenericBookDto>();
            if (searchType.Equals("book"))
            {
                 books = _mapper.Map<List<GenericBookDto>>(await _context.Books
                    .Where(b => b.Title.ToLower().Contains(searchValue))
                    .Take(MaxBookSearch).ToListAsync());
            } else if (searchType.Equals("ebook"))
            {
                books = _mapper.Map<List<GenericBookDto>>(await _context.EBooks
                    .Where(b => b.Title.ToLower().Contains(searchValue))
                    .Take(MaxBookSearch).ToListAsync());
            } else
            {
                books = _mapper.Map<List<GenericBookDto>>(await _context.AudioBooks
                    .Where(b => b.Title.ToLower().Contains(searchValue))
                    .Take(MaxBookSearch).ToListAsync());
            }

            return books;
        }

        public async Task<List<string>> SearchBookAuthors(string author)
        {
            var books = _mapper.Map<List<GenericBookDto>>(await _context.Books
                    .Where(b => b.Author.ToLower().Contains(author))
                    .Take(MaxAuthors).ToListAsync());
            var ebooks = _mapper.Map<List<GenericBookDto>>(await _context.EBooks
                    .Where(b => b.Author.ToLower().Contains(author))
                    .Take(MaxAuthors).ToListAsync());
            var audiobooks = _mapper.Map<List<GenericBookDto>>(await _context.AudioBooks
                    .Where(b => b.Author.ToLower().Contains(author))
                    .Take(MaxAuthors).ToListAsync());

            books.AddRange(ebooks);
            books.AddRange(audiobooks);

            var authorList = new List<string>();
            foreach (var book in books)
            {
                authorList.Add(book.Author);
            }

            return authorList;
        }

        public async Task<List<GenericBookDto>> GetBooksByAuthor(string author)
        {
            var books = _mapper.Map<List<GenericBookDto>>(await _context.Books
                    .Where(b => b.Author.ToLower().Contains(author))
                    .Take(MaxAuthors).ToListAsync());
            var ebooks = _mapper.Map<List<GenericBookDto>>(await _context.EBooks
                    .Where(b => b.Author.ToLower().Contains(author))
                    .Take(MaxAuthors).ToListAsync());
            var audiobooks = _mapper.Map<List<GenericBookDto>>(await _context.AudioBooks
                    .Where(b => b.Author.ToLower().Contains(author))
                    .Take(MaxAuthors).ToListAsync());

            books.AddRange(ebooks);
            books.AddRange(audiobooks);

            return books;
        }
    }
}
