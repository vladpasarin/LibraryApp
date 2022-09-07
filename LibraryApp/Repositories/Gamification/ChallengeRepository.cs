using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
    public class ChallengeRepository : GenericRepository<Challenge>, IChallengeRepository
    {
        private readonly IMapper _mapper;

        public ChallengeRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(ChallengeDto challengeDto)
        {
            var challenge = _mapper.Map<Challenge>(challengeDto);

            await Create(challenge);
            return await SaveChanges();
        }
    }
}
