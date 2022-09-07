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
    public class LibraryCardRepository : GenericRepository<LibraryCard>, ILibraryCardRepository
    {
        private readonly IMapper _mapper;

        public LibraryCardRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<LibraryCardDto>> GetAllLibraryCards()
        {
            var libraryCards = await _context.LibraryCards
                .ToListAsync();

            return _mapper.Map<IEnumerable<LibraryCardDto>>(libraryCards);
        }

        public async Task<LibraryCardDto> Get(int cardId)
        {
            var libraryCard = await _context.LibraryCards
                .Include(l => l.Checkouts)
                .FirstAsync(l => l.Id == cardId);

            return _mapper.Map<LibraryCardDto>(libraryCard);
        }

        public async Task<int> GetIdByUserId(int userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstAsync();

            return user.Id;
        }
    }
}
