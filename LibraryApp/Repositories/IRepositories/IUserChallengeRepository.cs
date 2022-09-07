using LibraryApp.DTOs;
using LibraryApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IUserChallengeRepository : IGenericRepository<UserChallenge>
    {
        public Task<UserChallengeDto> Get(int id);
        public Task<UserChallengeDto> GetByUserAndChallenge(int userId, int challengeId);
        public Task<List<UserChallengeDto>> GetUserChallenges(int userId);
    }
}
