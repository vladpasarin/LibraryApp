using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
    public class HoldRepository : GenericRepository<Hold>, IHoldRepository
    {
        private readonly IMapper _mapper;
        private readonly int holdExpireTerm = 30;

        public HoldRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Hold> GetEarliestHold(int assetId)
        {
            var hold = await _context.Holds
                .Include(h => h.Asset)
                .Include(h => h.LibraryCard)
                .Where(h => h.Asset.Id == assetId)
                .OrderBy(h => h.HoldPlaced)
                .FirstOrDefaultAsync();

            return hold;
        }

        public async Task<IEnumerable<HoldDto>> GetCurrentHolds(int assetId)
        {
            var holds = await _context.Holds
                .Include(h => h.Asset)
                .Where(h => h.Asset.Id == assetId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<HoldDto>>(holds);
        }

        public async Task<UserDto> GetCurrentHoldUser(int holdId)
        {
            var hold = await _context.Holds
                .Include(h => h.Asset)
                .Include(h => h.LibraryCard)
                .FirstAsync(h => h.Id == holdId);

            var cardId = hold.LibraryCard.Id;

            var user = await _context.Users
                .Include(u => u.LibraryCard)
                .FirstAsync(u => u.LibraryCardId == cardId);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<HoldDto> GetCurrentHoldPlaced(int holdId)
        {
            var hold = await _context.Holds
                .Include(h => h.Asset)
                .Include(h => h.LibraryCard)
                .FirstAsync(h => h.Id == holdId);

            return _mapper.Map<HoldDto>(hold);
        }

        public async Task<bool> PlaceHold(int assetId, int cardId)
        {
            var now = DateTime.UtcNow;

            var asset = await _context.Assets
                .Include(a => a.AvailabilityStatus)
                .FirstAsync(a => a.Id == assetId);

            var card = await _context.LibraryCards
                .FirstAsync(c => c.Id == cardId);

            var existingHold = await _context.Holds
                .Include(h => h.Asset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryCard.Id == cardId)
                .FirstOrDefaultAsync(h => h.Asset.Id == assetId);

            if (existingHold != null)
            {
                return false;
            }

            _context.Update(asset);
            if (asset.NrOfAvailableCopies < 1)
            {
                asset.AvailabilityStatus = await _context.AvailabilityStatuses
                    .FirstAsync(a => a.Name == "On Hold");
            }
            else
            {
                asset.NrOfAvailableCopies -= 1;
            }

            var hold = new Hold
            {
                HoldPlaced = now,
                Asset = asset,
                LibraryCard = card,
                UpdatedOn = now,
                HoldValidUntil = now.AddDays(holdExpireTerm)
            };

            await _context.AddAsync(hold);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
