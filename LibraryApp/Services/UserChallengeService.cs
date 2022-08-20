using AutoMapper;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class UserChallengeService : IUserChallengeService
    {
        private readonly IUserChallengeRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICheckoutHistoryRepository _checkoutHistoryRepository;
        private readonly IBookmarkService _bookmarkService;
        private readonly IRatingService _ratingService;

        public UserChallengeService(IUserChallengeRepository repo, IMapper mapper, ICheckoutHistoryRepository checkoutHistoryRepository, IBookmarkService bookmarkService, IRatingService ratingService)
        {
            _repo = repo;
            _mapper = mapper;
            _checkoutHistoryRepository = checkoutHistoryRepository;
            _bookmarkService = bookmarkService;
            _ratingService = ratingService;
        }

        public async Task<List<UserChallengeDto>> GetAll()
        {
            var userChallenges =  await _repo.GetAll();

            return _mapper.Map<List<UserChallengeDto>>(userChallenges);
        }

        public async Task<bool> UpdateProgress(int id, int userId)
        {
            var userChallenge = await Get(id);

            // Hardcoded based on the challenge id considered as challenge type
            switch (userChallenge.ChallengeId)
            {
                case 1:
                    userChallenge.Progress = (await _checkoutHistoryRepository.GetUserCheckoutHistory(userChallenge.UserId)).Count;
                    break;
                case 2:
                    userChallenge.Progress = (await _bookmarkService.GetUserBookmarkedAssets(userChallenge.UserId)).Count;
                    break;
                case 3:
                    userChallenge.Progress = (await _ratingService.GetUserRatings(userChallenge.UserId)).Count;
                    break;
                default:
                    break;
            }

            if (userChallenge.Progress > userChallenge.Threshold)
            {
                userChallenge.Completed = true;
                userChallenge.DateCompleted = System.DateTime.Now;
            }

            _repo.Update(_mapper.Map<UserChallenge>(userChallenge));
            return await _repo.SaveChanges();
        }


        public async Task<bool> CreateAsync(UserChallengeDto userChallengeDto)
        {
            var userChallenge = _mapper.Map<UserChallenge>(userChallengeDto);

            // Hardcoded based on the challenge id considered as challenge type
            switch(userChallenge.ChallengeId)
            {
                case 1:
                    userChallenge.Threshold = 0;
                    userChallenge.Progress = (await _checkoutHistoryRepository.GetUserCheckoutHistory(userChallenge.UserId)).Count;
                    break;
                case 2:
                    userChallenge.Threshold = 2;
                    userChallenge.Progress = (await _bookmarkService.GetUserBookmarkedAssets(userChallenge.UserId)).Count;
                    break;
                case 3:
                    userChallenge.Threshold = 2;
                    userChallenge.Progress = (await _ratingService.GetUserRatings(userChallenge.UserId)).Count;
                    break;
                default:
                    break;
            }
            
            if (userChallenge.Progress > userChallenge.Threshold)
            {
                userChallenge.Completed = true;
                userChallenge.DateCompleted = System.DateTime.Now;
            }

            await _repo.Create(userChallenge);
            return await _repo.SaveChanges();
        }

        public async Task<UserChallengeDto> Get(int id)
        {
            return await _repo.Get(id);
        }

        public async Task<UserChallengeDto> GetByUserAndChallenge(int userId, int challengeId)
        {
            return await _repo.GetByUserAndChallenge(userId, challengeId);
        }
    }
}
