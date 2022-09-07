using LibraryApp.DTOs;
using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface ILibraryCardRepository : IGenericRepository<LibraryCard>
    {
        public Task<LibraryCardDto> Get(int cardId);
        public Task<IEnumerable<LibraryCardDto>> GetAllLibraryCards();
        public Task<int> GetIdByUserId(int userId);
    }
}
