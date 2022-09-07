using LibraryApp.DTOs.Assets;
using LibraryApp.Entities.Assets.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IAssetTagService
    {
        Task<bool> Add(AssetTag newAssetTag);
        Task<AssetTag> Get(int id);
        Task<IEnumerable<AssetTag>> GetAll();
        Task<IEnumerable<TagDto>> GetTagsByAssetId(int assetId);
        Task<IEnumerable<AssetDto>> GetAssetsByTagId(int tagId);
    }
}
