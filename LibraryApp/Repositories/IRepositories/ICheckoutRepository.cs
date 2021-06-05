using LibraryApp.DTOs;
using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface ICheckoutRepository : IGenericRepository<Checkout>
    {
        Task<bool> Add(CheckoutDto newCheckout);
        Task<CheckoutDto> Get(int id);
        Task<CheckoutDto> GetLatestCheckout(int assetId);
        Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistory(int assetId);
        Task<bool> CheckInItem(int assetId);
        Task<bool> CheckOutItem(int assetId, int cardId);
        Task<bool> IsCheckedOut(int assetId);
        Task<UserDto> GetCurrentUser(int assetId);
    }
}
