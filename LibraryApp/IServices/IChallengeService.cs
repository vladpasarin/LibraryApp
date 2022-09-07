using LibraryApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IChallengeService
    {
        Task<bool> CreateAsync(ChallengeDto challengeDto);
        Task<ChallengeDto> Get(int id);
        Task<List<ChallengeDto>> GetAll();
        Task<bool> StartChallenge(int id, int userId);
        /* Moved to UserChallenge
        Task<bool> GetBorrowChallengeStatus(int userId);
        Task<bool> GetBookmarkChallengeStatus(int userId);
        Task<bool> GetRatingChallengeStatus(int userId);
        Task<bool> GetChallengesStatus(int userId);
        */
    }
}
