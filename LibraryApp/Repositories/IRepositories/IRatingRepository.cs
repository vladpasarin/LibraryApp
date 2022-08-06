using LibraryApp.DTOs;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        public Task<List<RatingDto>> GetAssetRatings(int assetId);
        public Task<List<RatingDto>> GetUserRatings(int userId);
        public Task<bool> Add(RatingDto ratingDto);
        public Task<RatingDto> RatingExists(int userId, int assetId);
    }
}
