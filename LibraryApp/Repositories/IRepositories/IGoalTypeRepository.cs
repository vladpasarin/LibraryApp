using LibraryApp.DTOs;
using LibraryApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IGoalTypeRepository : IGenericRepository<GoalType>
    {
        public Task<bool> CreateAsync(GoalTypeDto goalTypeDto);
        public Task<List<GoalTypeDto>> GetAllGoalTypes();
    }
}
