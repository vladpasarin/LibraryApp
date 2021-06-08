using LibraryApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IHoldService
    {
        Task<HoldDto> GetEarliestHold(int assetId);
        Task<IEnumerable<HoldDto>> GetCurrentHolds(int assetId);
        Task<UserDto> GetCurrentHoldUser(int holdId);
        Task<HoldDto> GetCurrentHoldPlaced(int holdId);
        Task<bool> PlaceHold(int assetId, int cardId);
    }
}
