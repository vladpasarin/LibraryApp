using LibraryApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IUserChallengeService
    {
        Task<List<UserChallengeDto>> GetAll();
        Task<UserChallengeDto> Get(int id);
        Task<bool> CreateAsync(UserChallengeDto userChallengeDto);
        Task<UserChallengeDto> GetByUserAndChallenge(int userId, int challengeId);
        Task<bool> UpdateProgress(int id, int userId);
    }
}
