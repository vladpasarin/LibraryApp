using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface ICheckoutHistoryRepository
    {
        public Task<List<CheckoutHistory>> GetUserCheckoutHistory(int userId);
    }
}
