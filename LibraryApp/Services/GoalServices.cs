using AutoMapper;
using LibraryApp.DTOs;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class GoalServices : IGoalServices
    {
        private readonly IGoalRepository _repo;
        private readonly IBookmarkService _bookmarkService;
        private readonly ICheckoutHistoryRepository _checkoutHistoryRepository;
        private readonly IRatingService _ratingService;
        private readonly IGoalTypeRepository _goalTypeRepository;
        private readonly IMapper _mapper;

        public GoalServices(IGoalRepository repo, IBookmarkService bookmarkService, ICheckoutHistoryRepository checkoutHistoryRepository, IRatingService ratingService, IMapper mapper)
        {
            _repo = repo;
            _bookmarkService = bookmarkService;
            _checkoutHistoryRepository = checkoutHistoryRepository;
            _ratingService = ratingService;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(GoalDto goalDto)
        {
            return await _repo.CreateAsync(goalDto);
        }

        public async Task<bool> Delete(int id)
        {
            var goal = await _repo.GetById(id);
            _repo.Delete(goal);
            return await _repo.SaveChanges();
        }

        public async Task<List<GoalDto>> GetAll()
        {
            return _mapper.Map<List<GoalDto>>(await _repo.GetAll());
        }

        public async Task<GoalDto> GetById(int id)
        {
            return _mapper.Map<GoalDto>(await _repo.GetById(id));
        }

        public async Task<List<GoalTypeDto>> GetGoalTypes()
        {
            return await _goalTypeRepository.GetAllGoalTypes();
        }

        public async Task<List<GoalDto>> GetUserGoals(int userId)
        {
            return await _repo.GetUserGoals(userId);
        }

        public async Task<bool> UpdateGoal(int id)
        {
            var goal = await _repo.GetById(id);

            switch (goal.GoalType.Id)
            {
                case 1:
                    goal.Progress = (await _checkoutHistoryRepository.GetUserCheckoutHistory(goal.User.Id)).Count;
                    break;
                case 2:
                    goal.Progress = (await _bookmarkService.GetUserBookmarkedAssets(goal.User.Id)).Count;
                    break;
                case 3:
                    goal.Progress = (await _ratingService.GetUserRatings(goal.User.Id)).Count;
                    break;
                default:
                    break;
            }
            
            _repo.Update(goal);
            return await _repo.SaveChanges();
        }
    }
}
