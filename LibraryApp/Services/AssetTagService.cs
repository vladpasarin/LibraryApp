using AutoMapper;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities.Assets.Tags;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class AssetTagService : IAssetTagService
    {
        private readonly IAssetTagRepository _repo;

        public AssetTagService(IAssetTagRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Add(AssetTag newAssetTag)
        {
            await _repo.Create(newAssetTag);
            return await _repo.SaveChanges();
        }

        public async Task<AssetTag> Get(int id)
        {
            return await _repo.Get(id);
        }

        public async Task<IEnumerable<AssetTag>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<IEnumerable<AssetDto>> GetAssetsByTagId(int tagId)
        {
            return await _repo.GetAssetsByTagId(tagId);
        }

        public async Task<IEnumerable<TagDto>> GetTagsByAssetId(int assetId)
        {
            return await _repo.GetTagsByAssetId(assetId);
        }
    }
}
