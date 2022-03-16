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
        Task<IEnumerable<EBookDto>> GetAllEBooks();
        Task<IEnumerable<AudioBookDto>> GetAllAudioBooks();
        Task<EBookDto> GetEBook(int id);
        Task<AudioBookDto> GetAudioBook(int id);
        Task<GenericBookDto> GetGenericBook(int assetId);
        Task<bool> AddEBook(EBookDto newEBook);
        Task<bool> AddAudioBook(AudioBookDto newAudioBook);
        Task<List<GenericBookDto>> GetAllGenericBooks();
    }
}
