using LibraryApp.DTOs;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICheckoutRepository _repo;

        public CheckoutService(ICheckoutRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Add(CheckoutDto newCheckout)
        {
            return await _repo.Add(newCheckout);
        }

        public async Task<bool> CheckInItem(int assetId)
        {
            return await _repo.CheckInItem(assetId);
        }

        public async Task<bool> CheckOutItem(int assetId, int cardId)
        {
            return await _repo.CheckOutItem(assetId, cardId);
        }

        public async Task<CheckoutDto> Get(int id)
        {
            return await _repo.Get(id);
        }

        public async Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistory(int assetId)
        {
            return await _repo.GetCheckoutHistory(assetId);
        }

        public async Task<UserDto> GetCurrentUser(int assetId)
        {
            return await _repo.GetCurrentUser(assetId);
        }

        public async Task<CheckoutDto> GetLatestCheckout(int assetId)
        {
            return await _repo.GetLatestCheckout(assetId);
        }

        public async Task<bool> IsCheckedOut(int assetId)
        {
            return await _repo.IsCheckedOut(assetId);
        }

        public async Task<bool> GetByCardAndAsset(int assetId, int cardId)
        {
            var checkout = await _repo.GetByCardAndAsset(assetId, cardId);
            if (checkout == null)
            {
                return false;
            }
            else return true;
        }
    }
}
