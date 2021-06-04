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
    }
}
