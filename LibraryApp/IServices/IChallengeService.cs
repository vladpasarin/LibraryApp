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
    }
}
