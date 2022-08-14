using LibraryApp.Data;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Gamification
{
    public class UserChallengeRepository : GenericRepository<UserChallenge>, IUserChallengeRepository
    {
        public UserChallengeRepository(LibraryDbContext context) : base(context)
        {
        }

        public Task<UserChallengeDto> Get(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
