using LibraryApp.DTOs.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAll();
        Task<BookDto> Get(int id);
        Task<bool> Add(BookDto newBook);
    }
}
