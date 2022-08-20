using LibraryApp.Data;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
    public class CheckoutHistoryRepository : GenericRepository<CheckoutHistory>, ICheckoutHistoryRepository
    {
        public CheckoutHistoryRepository(LibraryDbContext context) : base(context)
        {

        }

        public async Task<List<CheckoutHistory>> GetUserCheckoutHistory(int userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            var history = await _context.CheckoutHistories
                .Where(ch => ch.LibraryCard.Id == user.LibraryCardId)
                .ToListAsync();

            return history;
        }
    }
}
