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
    public class CheckoutRepository : GenericRepository<Checkout>, ICheckoutRepository
    {
        private readonly IMapper _mapper;
        private readonly IHoldRepository _holdRepo;
        private const int dueDays = 14;

        public CheckoutRepository(LibraryDbContext context, IMapper mapper, IHoldRepository holdRepo) : base(context)
        {
            _mapper = mapper;
            _holdRepo = holdRepo;
        }

        public async Task<bool> Add(CheckoutDto newCheckout)
        {
            var checkout = _mapper.Map<Checkout>(newCheckout);
            await _context.AddAsync(checkout);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CheckoutDto> Get(int id)
        {
            var checkout = await _context.Checkouts
                .Include(c => c.Asset)
                .Include(c => c.LibraryCard)
                .FirstAsync(c => c.Id == id);
            return _mapper.Map<CheckoutDto>(checkout);
        }

        public async Task<CheckoutDto> GetLatestCheckout(int assetId)
        {
            var latest = await _context.Checkouts
                .Where(c => c.Asset.Id == assetId)
                .OrderByDescending(c => c.CheckedOutSince)
                .FirstAsync();
            return _mapper.Map<CheckoutDto>(latest);
        }

        public async Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistory(int assetId)
        {
            var history = await _context.CheckoutHistories
                .Include(ch => ch.Asset)
                .Include(ch => ch.LibraryCard)
                .Where(ch => ch.Asset.Id == assetId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CheckoutHistoryDto>>(history);
        }

        public async Task<bool> IsCheckedOut(int assetId)
        {
            return await _context.Checkouts
                .AnyAsync(a => a.Asset.Id == assetId);
        }

        
        public async Task<bool> CheckInItem(int assetId)
        {
            var now = DateTime.UtcNow;
            var asset = await _context.Assets
                .FirstAsync(a => a.Id == assetId);
            _context.Update(asset);

            var checkout = await _context.Checkouts
                .Include(c => c.Asset)
                .Include(c => c.LibraryCard)
                .FirstAsync(c => c.Asset.Id == assetId);
            if (checkout != null)
            {
                _context.Remove(checkout);
            }

            var history = await _context.CheckoutHistories
                .Include(ch => ch.Asset)
                .Include(ch => ch.LibraryCard)
                .FirstAsync(ch => ch.Asset.Id == assetId && ch.CheckedIn == null);
            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }

            asset.NrOfAvailableCopies += 1;
            var wasCheckedOut = await CheckoutEarliestHold(assetId);
            if (wasCheckedOut)
            {
                return true;
            }

            asset.AvailabilityStatus = await _context.AvailabilityStatuses
                .FirstAsync(a => a.Name == "Available");

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CheckOutItem(int assetId, int cardId)
        {
            var now = DateTime.UtcNow;

            var asset = await _context.Assets
                .Include(a => a.AvailabilityStatus)
                .FirstAsync(a => a.Id == assetId);
            _context.Update(asset);
            if (asset.NrOfAvailableCopies > 0)
            {
                asset.NrOfAvailableCopies -= 1;
            }
            else if (asset.NrOfAvailableCopies < 1)
            {
                asset.AvailabilityStatus = await _context.AvailabilityStatuses
                .FirstAsync(a => a.Name == "On Hold");
                return await _holdRepo.PlaceHold(assetId, cardId);
            }

            var libraryCard = await _context.LibraryCards
                .Include(c => c.Checkouts)
                .FirstAsync(l => l.Id == cardId);
            libraryCard.MaxCheckout = true;

            var checkout = new Checkout
            {
                Asset = asset,
                LibraryCard = libraryCard,
                CheckedOutSince = now,
                CheckedOutUntil = now.AddDays(dueDays)
            };
            await _context.AddAsync(checkout);

            var history = new CheckoutHistory
            {
                CheckedOut = now,
                Asset = asset,
                LibraryCard = libraryCard
            };
            await _context.AddAsync(history);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckoutEarliestHold(int assetId)
        {
            var hold = await _holdRepo.GetEarliestHold(assetId);

            if (hold == null)
            {
                return false;
            }

            var card = hold.LibraryCard;
            _context.Remove(hold);
            await _context.SaveChangesAsync();

            return await CheckOutItem(assetId, card.Id);
        }

        public async Task<UserDto> GetCurrentUser(int assetId)
        {
            var history = await _context.CheckoutHistories
                .Include(h => h.Asset)
                .Include(h => h.LibraryCard)
                .Where(h => h.Asset.Id == assetId)
                .OrderByDescending(h => h.CheckedIn)
                .FirstAsync();

            var user = await _context.Users
                .Include(u => u.LibraryCard)
                .FirstAsync(u => u.LibraryCardId == history.LibraryCard.Id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<CheckoutDto> GetByCardAndAsset(int assetId, int cardId)
        {
            var checkout = await _context.Checkouts
                .Include(c => c.Asset)
                .Include(c => c.LibraryCard)
                .Where(c => c.Asset.Id == assetId)
                .Where(c => c.LibraryCard.Id == cardId)
                .FirstOrDefaultAsync();

            return _mapper.Map<CheckoutDto>(checkout);
        }
    }
}
