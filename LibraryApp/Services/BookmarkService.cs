using AutoMapper;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _repo;
        private readonly IMapper _mapper;

        public BookmarkService(IBookmarkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> Add(BookmarkDto newBookmark)
        {
            var bookmark = _mapper.Map<Bookmark>(newBookmark);
            await _repo.Create(bookmark);
            return await _repo.SaveChanges();
        }

        public async Task<BookmarkDto> Get(int id)
        {
            return await _repo.Get(id);
        }

        public async Task<IEnumerable<BookmarkDto>> GetAll()
        {
            var bookmarks = await _repo.GetAll();
            return _mapper.Map<IEnumerable<BookmarkDto>>(bookmarks);
        }

        public async Task<BookmarkDto> FindBookmarkByUserAndAsset(int userId, int assetId)
        {
            return await _repo.FindBookmarkBy(userId, assetId);
        }

        public async Task<bool> Delete(BookmarkDto deletedBookmark)
        {
            var bookmark = _mapper.Map<Bookmark>(deletedBookmark);
            _repo.Delete(bookmark);
            return await _repo.SaveChanges();
        }
    }
}
