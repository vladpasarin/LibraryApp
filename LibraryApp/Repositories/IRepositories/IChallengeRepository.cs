using LibraryApp.DTOs;
using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IChallengeRepository : IGenericRepository<Challenge>
    {
        public Task<bool> CreateAsync(ChallengeDto challengeDto);
    }
}
