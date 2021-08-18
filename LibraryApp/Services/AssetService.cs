using AutoMapper;
using LibraryApp.DTOs;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repo;
        private readonly IMapper _mapper;

        public AssetService(IAssetRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> Add(AssetDto newAssetDto)
        {
            var newAsset = _mapper.Map<Asset>(newAssetDto);
            await _repo.Create(newAsset);
            return  await _repo.SaveChanges();
        }

        public async Task<AssetDto> Get(int id)
        {
            return await _repo.Get(id);
        }

        public async Task<IEnumerable<AssetDto>> GetAll()
        {
            return await _repo.GetAllAssets();
        }

        public async Task<AvailabilityStatusDto> GetStatus(int assetId)
        {
            return await _repo.GetStatus(assetId);
        }
    }
}
