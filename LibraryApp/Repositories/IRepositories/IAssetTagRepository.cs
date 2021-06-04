using LibraryApp.DTOs.Assets;
using LibraryApp.Entities.Assets.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IAssetTagRepository : IGenericRepository<AssetTag>
    {
        public Task<AssetTag> Get(int id);
        public Task<IEnumerable<TagDto>> GetTagsByAssetId(int assetId);
        public Task<IEnumerable<AssetDto>> GetAssetsByTagId(int tagId);
    }
}
