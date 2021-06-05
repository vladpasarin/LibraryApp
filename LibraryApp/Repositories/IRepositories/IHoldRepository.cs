using LibraryApp.DTOs;
using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IHoldRepository : IGenericRepository<Hold>
    {
        public Task<HoldDto> GetEarliestHold(int assetId);
        public Task<IEnumerable<HoldDto>> GetCurrentHolds(int assetId);
        public Task<UserDto> GetCurrentHoldUser(int holdId);
        public Task<HoldDto> GetCurrentHoldPlaced(int holdId);
        public Task<bool> PlaceHold(int assetId, int cardId);
    }
}
