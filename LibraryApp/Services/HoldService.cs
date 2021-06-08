using LibraryApp.DTOs;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class HoldService : IHoldService
    {
        public readonly IHoldRepository _repo;

        public HoldService(IHoldRepository repo)
        {
            _repo = repo;
        }

        public async Task<HoldDto> GetCurrentHoldPlaced(int holdId)
        {
            return await _repo.GetCurrentHoldPlaced(holdId);
        }

        public async Task<IEnumerable<HoldDto>> GetCurrentHolds(int assetId)
        {
            return await _repo.GetCurrentHolds(assetId);
        }

        public async Task<UserDto> GetCurrentHoldUser(int holdId)
        {
            return await _repo.GetCurrentHoldUser(holdId);
        }

        public async Task<HoldDto> GetEarliestHold(int assetId)
        {
            return await _repo.GetEarliestHold(assetId);
        }

        public async Task<bool> PlaceHold(int assetId, int cardId)
        {
            return await _repo.PlaceHold(assetId, cardId);
        }
    }
}
