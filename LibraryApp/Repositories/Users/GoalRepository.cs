using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Users
{
    public class GoalRepository : GenericRepository<Goal>, IGoalRepository
    {
        private readonly IMapper _mapper;
        public GoalRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(GoalDto goalDto)
        {
            var goal = _mapper.Map<Goal>(goalDto);

            await Create(goal);
            return await SaveChanges();
        }

        public async Task<Goal> GetById(int id)
        {
            return await _context.Goals
                .Where(g => g.Id == id)
                .Include(g => g.User)
                .Include(g => g.GoalType)
                .FirstOrDefaultAsync();
        }

        public async Task<List<GoalDto>> GetUserGoals(int userId)
        {
            var userGoals = await _context.Goals
                .Where(g => g.User.Id == userId)
                .ToListAsync();

            return _mapper.Map<List<GoalDto>>(userGoals);
        }
    }
}
