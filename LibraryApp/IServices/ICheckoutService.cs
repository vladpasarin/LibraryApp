using LibraryApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface ICheckoutService
    {
        Task<bool> Add(CheckoutDto newCheckout);
        Task<CheckoutDto> Get(int id);
        Task<CheckoutDto> GetLatestCheckout(int assetId);
        Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistory(int assetId);
        Task<bool> CheckInItem(int assetId);
        Task<bool> CheckOutItem(int assetId, int cardId);
        Task<bool> IsCheckedOut(int assetId);
        Task<UserDto> GetCurrentUser(int assetId);
        Task<bool> GetByCardAndAsset(int assetId, int cardId);
    }
}
