using AutoMapper;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserChallengeService _userChallengeService;

        public ChallengeService(IChallengeRepository repo, IMapper mapper, IUserChallengeService userChallengeService)
        {
            _repo = repo;
            _mapper = mapper;
            _userChallengeService = userChallengeService;
        }

        public async Task<bool> CreateAsync(ChallengeDto challengeDto)
        {
            return await _repo.CreateAsync(challengeDto);
        }

        public async Task<ChallengeDto> Get(int id)
        {
            var challenge =  await _repo.FindById(id);

            return _mapper.Map<ChallengeDto>(challenge);
        }

        public async Task<List<ChallengeDto>> GetAll()
        {
            var challengeList = await _repo.GetAll();

            return _mapper.Map<List<ChallengeDto>>(challengeList);
        }

        /*
        public async Task<bool> GetBookmarkChallengeStatus(int userId)
        {
            var bookmarks = await _bookmarkService.GetUserBookmarkedAssets(userId);
            var challenge = _mapper.Map<Challenge>(await Get(2));

            if (challenge.Started == false)
            {
                return challenge.Completed;
            }

            if (challenge.Completed == true)
            {
                return challenge.Completed;
            }

            if (bookmarks.Count > 2)
            {
                challenge.Completed = true;
                challenge.DateCompleted = System.DateTime.Now;
                await _repo.SaveChanges();
            }

            return challenge.Completed;
        }

        public async Task<bool> GetBorrowChallengeStatus(int userId)
        {
            var history = await _checkoutHistoryRepo.GetUserCheckoutHistory(userId);
            var challenge = _mapper.Map<Challenge>(await Get(1));

            if (challenge.Started == false)
            {
                return challenge.Completed;
            }

            if (challenge.Completed == true)
            {
                return challenge.Completed;
            }

            if (history.Count > 0)
            {
                challenge.Completed = true;
                challenge.DateCompleted = System.DateTime.Now;
                await _repo.SaveChanges();
            }

            return challenge.Completed;
        }

        public async Task<bool> GetChallengesStatus(int userId)
        {
            await GetBorrowChallengeStatus(userId);
            await GetBookmarkChallengeStatus(userId);
            await GetRatingChallengeStatus(userId);

            return true;
        }

        public async Task<bool> GetRatingChallengeStatus(int userId)
        {
            var ratings = await _ratingService.GetUserRatings(userId);
            var challenge = _mapper.Map<Challenge>(await Get(3));

            if (challenge.Started == false)
            {
                return challenge.Completed;
            }

            if (challenge.Completed == true)
            {
                return challenge.Completed;
            }

            if (ratings.Count > 2)
            {
                challenge.Completed = true;
                challenge.DateCompleted = System.DateTime.Now;
                await _repo.SaveChanges();
            }

            return challenge.Completed;
        }
        */

        public async Task<bool> StartChallenge(int id, int userId)
        {
            var challenge = await _repo.FindById(id);
            int threshold;

            if (id == 1)
            {
                threshold = 0;
            } else
            {
                threshold = 2;
            }

            var userChallengeDto = new UserChallengeDto()
            {
                UserId = userId,
                ChallengeId = id,
                Threshold = threshold,
                DateStarted = System.DateTime.Now
            };

            //var history = await _checkoutHistoryRepo.GetUserCheckoutHistory(userId);

            /*var userChallengeDto = new UserChallengeDto()
            {
                UserId = userId,
                ChallengeId = id,
                Progress = history.Count
            };*/

            /*if (history.Count == challenge.Threshold)
            {
                challenge.Completed = true;
                challenge.DateCompleted = System.DateTime.Now;
            }*/

            challenge.Started = true;
            _repo.Update(challenge);
            return await _userChallengeService.CreateAsync(userChallengeDto);
        }
    }
}
