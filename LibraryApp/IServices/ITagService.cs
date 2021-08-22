using LibraryApp.DTOs.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface ITagService
    {
        Task<bool> Add(TagDto newTagDto);
        Task<TagDto> Get(int id);
        Task<IEnumerable<TagDto>> GetAll();
    }
}
