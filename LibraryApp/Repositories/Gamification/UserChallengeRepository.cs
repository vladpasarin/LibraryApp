using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Gamification
{
    public class UserChallengeRepository : GenericRepository<UserChallenge>, IUserChallengeRepository
    {
        private readonly IMapper _mapper;

        public UserChallengeRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<UserChallengeDto> Get(int id)
        {
            var userChallenge = await _context.UserChallenges
                .Where(uc => uc.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<UserChallengeDto>(userChallenge);
        }

        public async Task<UserChallengeDto> GetByUserAndChallenge(int userId, int challengeId)
        {
            var userChallenge = await _context.UserChallenges
                .Where(uc => uc.UserId == userId && uc.ChallengeId == challengeId)
                .FirstOrDefaultAsync();

            return _mapper.Map<UserChallengeDto>(userChallenge);
        }

        public Task<List<UserChallengeDto>> GetUserChallenges(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
