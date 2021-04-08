using LibraryApp.DTOs;
using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface ICheckoutRepository : IGenericRepository<CheckoutDto>
    {
        Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistory(int id);
        Task<bool> PlaceHold(int assetId, int libraryCardId);
        Task<bool> CheckInItem(int assetId);
        Task<bool> CheckoutItem(int assetId, int libraryCardId);
        Task<bool> IsCheckedOut(int assetId);
        Task<string> GetCurrentUser(int assetId);
        Task<IEnumerable<HoldDto>> GetCurrentHolds(int assetId);
    }
}
