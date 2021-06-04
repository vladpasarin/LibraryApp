using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using LibraryApp.Entities.Assets.Tags;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Assets
{
    public class AssetTagRepository : GenericRepository<AssetTag>, IAssetTagRepository
    {
        private readonly IMapper _mapper;

        public AssetTagRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<AssetTag> Get(int id)
        {
            return await _context.AssetTags
                .Include(a => a.Asset)
                .Include(a => a.Tag)
                .FirstAsync(a => a.Id == id);
        }
        
        public async Task<IEnumerable<AssetDto>> GetAssetsByTagId(int tagId)
        {
            var assetTags = await _context.AssetTags
                .Include(a => a.Asset)
                .Include(a => a.Tag)
                .Where(a => a.TagId == tagId).ToListAsync();
            IEnumerable<Asset> assets = Enumerable.Empty<Asset>();
            foreach(var asset in assetTags)
            {
                assets.Append(asset.Asset);
            }
            return _mapper.Map<IEnumerable<AssetDto>>(assets);
        }
        
        public async Task<IEnumerable<TagDto>> GetTagsByAssetId(int assetId)
        {
            var assetTags = await _context.AssetTags
                .Include(a => a.Asset)
                .Include(a => a.Tag)
                .Where(a => a.AssetId == assetId).ToListAsync();
            IEnumerable<Tag> tags = Enumerable.Empty<Tag>();
            foreach (var tag in assetTags)
            {
                tags.Append(tag.Tag);
            }
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }
    }
}
