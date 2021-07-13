using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
    public class AssetRepository : GenericRepository<Asset>, IAssetRepository
    {
        private readonly IMapper _mapper;

        public AssetRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<AssetDto> Get(int id)
        {
            var asset = await _context.Assets
                .Include(a => a.AvailabilityStatus)
                .Include(a => a.AssetTags)
                .FirstAsync(a => a.Id == id);
            return _mapper.Map<AssetDto>(asset);
        }

        public async Task<IEnumerable<AssetDto>> GetAllAssets()
        {
            IEnumerable<Asset> assets = new List<Asset>();
            assets = await _context.Assets
                .Include(a => a.AvailabilityStatus)
                .Include(a => a.AssetTags)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AssetDto>>(assets);
        }
    }
}
