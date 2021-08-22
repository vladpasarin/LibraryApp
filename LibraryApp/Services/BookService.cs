using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;

namespace LibraryApp.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<BookDto> Get(int id)
        {
            return await _repo.Get(id);
        }

        public async Task<IEnumerable<BookDto>> GetAll()
        {
            var books = await _repo.GetAll();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<bool> Add(BookDto newBook)
        {
            var book = _mapper.Map<Book>(newBook);
            await _repo.Create(book);
            return await _repo.SaveChanges();
        }

        public async Task<IEnumerable<EBookDto>> GetAllEBooks()
        {
            var eBooks = await _repo.GetAllEBooks();
            return _mapper.Map<IEnumerable<EBookDto>>(eBooks);
        }

        public async Task<IEnumerable<AudioBookDto>> GetAllAudioBooks()
        {
            var audioBooks = await _repo.GetAllAudioBooks();
            return _mapper.Map<IEnumerable<AudioBookDto>>(audioBooks);
        }

        public async Task<EBookDto> GetEBook(int id)
        {
            return await _repo.GetEBook(id);
        }

        public async Task<AudioBookDto> GetAudioBook(int id)
        {
            return await _repo.GetAudioBook(id);
        }

        public async Task<bool> AddEBook(EBookDto newEBook)
        {
            return await _repo.AddEBook(newEBook);
        }

        public async Task<bool> AddAudioBook(AudioBookDto newAudioBook)
        {
            return await _repo.AddAudioBook(newAudioBook);
        }

        public async Task<GenericBookDto> GetGenericBook(int assetId)
        {
            return await _repo.GetGenericBook(assetId);
        }
    }
}
