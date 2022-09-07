using LibraryApp.DTOs;
using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IHoldService
    {
        Task<Hold> GetEarliestHold(int assetId);
        Task<IEnumerable<HoldDto>> GetCurrentHolds(int assetId);
        Task<UserDto> GetCurrentHoldUser(int holdId);
        Task<HoldDto> GetCurrentHoldPlaced(int holdId);
        Task<bool> PlaceHold(int assetId, int cardId);
        Task<HoldDto> GetUserHoldOnAsset(int assetId, int cardId);
    }
}
