using LibraryApp.DTOs;
using LibraryApp.Entities;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IUserChallengeRepository : IGenericRepository<UserChallenge>
    {
        public Task<UserChallengeDto> Get(int id);
    }
}
