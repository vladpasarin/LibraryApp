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
        public Task<EBookDto> GetEBook(int id);
        public Task<AudioBookDto> GetAudioBook(int id);
        public Task<bool> AddEBook(EBookDto newEBook);
        public Task<bool> AddAudioBook(AudioBookDto newAudioBook);
        public void DeleteEBook(EBookDto eBook);
        public void DeleteAudioBook(AudioBookDto audioBook);
        public Task<List<EBook>> GetAllEBooks();
        public Task<List<AudioBook>> GetAllAudioBooks();
        public void UpdateEBook(EBook eBook);
        public void UpdateAudioBook(AudioBook audioBook);
    }
}
