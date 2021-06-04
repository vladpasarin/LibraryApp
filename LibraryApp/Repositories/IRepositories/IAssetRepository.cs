﻿using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IAssetRepository: IGenericRepository<Asset>
    {
        public Task<AssetDto> Get(int id);
    }
}
