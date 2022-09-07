using LibraryApp.DTOs;
using LibraryApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IGoalRepository : IGenericRepository<Goal>
    {
        public Task<bool> CreateAsync(GoalDto goalDto);
        //public Task<bool> UpdateGoal(int id);
        public Task<List<GoalDto>> GetUserGoals(int userId);
        public Task<Goal> GetById(int id);
    }
}
