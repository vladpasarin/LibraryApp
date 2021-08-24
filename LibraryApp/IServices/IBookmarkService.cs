using LibraryApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IBookmarkService
    {
        Task<BookmarkDto> Get(int id);
        Task<bool> Add(BookmarkDto newBookmark);
        Task<IEnumerable<BookmarkDto>> GetAll();
        Task<BookmarkDto> FindBookmarkByUserAndAsset(int userId, int assetId);
        Task<bool> Delete(BookmarkDto deletedBookmark);
        Task<bool> Exists(int id);
    }
}
