using LibraryApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface ILibraryCardService
    {
        Task<IEnumerable<LibraryCardDto>> GetAllLibraryCards();
        Task<LibraryCardDto> Get(int cardId);
        Task<bool> Add(LibraryCardDto newLibraryCard);
    }
}
