using LibraryApp.DTOs;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _repo;
        public RatingService(IRatingRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Add(RatingDto ratingDto)
        {
            return await _repo.Add(ratingDto);
        }

        public async Task<List<RatingDto>> GetAssetRatings(int assetId)
        {
            return await _repo.GetAssetRatings(assetId);
        }

        public async Task<List<RatingDto>> GetUserRatings(int userId)
        {
            return await _repo.GetUserRatings(userId);
        }

        public async Task<RatingDto> RatingExists(int userId, int assetId)
        {
            return await _repo.RatingExists(userId, assetId);
        }
    }
}
