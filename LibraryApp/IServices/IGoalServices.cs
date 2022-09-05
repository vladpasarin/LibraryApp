using LibraryApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IGoalServices
    {
        Task<bool> CreateAsync(GoalDto goalDto);
        Task<bool> UpdateGoal(int id);
        Task<List<GoalDto>> GetUserGoals(int userId);
        Task<GoalDto> GetById(int id);
        Task<List<GoalDto>> GetAll();
        Task<bool> Delete(int id);
        Task<List<GoalTypeDto>> GetGoalTypes();
    }
}
