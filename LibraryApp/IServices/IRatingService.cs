using LibraryApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IRatingService
    {
        Task<List<RatingDto>> GetAssetRatings(int assetId);
        Task<List<RatingDto>> GetUserRatings(int userId);
        Task<bool> Add(RatingDto ratingDto);
        Task<RatingDto> RatingExists(int userId, int assetId);
        Task<bool> UpdateRating(int ratingId, RatingDto ratingDto);
    }
}
