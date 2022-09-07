using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Users
{
    public class GoalTypeRepository : GenericRepository<GoalType>, IGoalTypeRepository
    {
        private readonly IMapper _mapper;
        public GoalTypeRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(GoalTypeDto goalTypeDto)
        {
            var goalType = _mapper.Map<GoalType>(goalTypeDto);
            await Create(goalType);
            return await SaveChanges();
        }

        public async Task<List<GoalTypeDto>> GetAllGoalTypes()
        {
            var goalTypes = await GetAll();

            return _mapper.Map<List<GoalTypeDto>>(goalTypes);
        }
    }
}
