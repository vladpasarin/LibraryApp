using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IBookRepository: IGenericRepository<Book>
    {
        public Task<BookDto> Get(int id);
        //public Task<bool> Add(BookDto newBookDto);
    }
}
