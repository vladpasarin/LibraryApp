using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Users
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private readonly IMapper _mapper;
        public RatingRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> Add(RatingDto ratingDto)
        {
            var rating = _mapper.Map<Rating>(ratingDto);

            await Create(rating);
            return await SaveChanges();
        }

        public async Task<List<RatingDto>> GetAssetRatings(int assetId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.AssetId == assetId)
                .ToListAsync();

            return _mapper.Map<List<RatingDto>>(ratings);
        }

        public async Task<List<RatingDto>> GetUserRatings(int userId)
        {
            var ratings = await _context.Ratings
                 .Where(r => r.UserId == userId)
                 .ToListAsync();

            return _mapper.Map<List<RatingDto>>(ratings);
        }

        public async Task<RatingDto> RatingExists(int userId, int assetId)
        {
            var rating = await _context.Ratings
                .Where(r => r.UserId == userId)
                .Where(r => r.AssetId == assetId)
                .FirstOrDefaultAsync();

            if (rating == null)
                return null;
            return _mapper.Map<RatingDto>(rating);
        }

        public async Task<bool> UpdateRating(int ratingId, RatingDto ratingDto)
        {
            var rating = await _context.Ratings
                .Where(r => r.Id == ratingId)
                .FirstOrDefaultAsync();

            if (rating == null)
                return false;

            rating.Score = ratingDto.Score;
            rating.Review = ratingDto.Review;

            Update(rating);
            return await SaveChanges();
        }
    }
}
