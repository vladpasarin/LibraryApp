using LibraryApp.DTOs.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IAssetService
    {
        Task<bool> Add(AssetDto newAssetDto);
        Task<AssetDto> Get(int id);
        Task<IEnumerable<AssetDto>> GetAll();
    }
}
