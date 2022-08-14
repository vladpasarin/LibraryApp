using AutoMapper;
using LibraryApp.DTOs;
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

        public ChallengeService(IChallengeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
    }
}
