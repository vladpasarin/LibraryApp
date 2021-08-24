using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Users
{
    public class BookmarkRepository : GenericRepository<Bookmark>, IBookmarkRepository
    {
        private readonly IMapper _mapper;

        public BookmarkRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<BookmarkDto> Get(int id)
        {
            var bookmark = await _context.Bookmarks
                .AsNoTracking()
                .Include(b => b.Asset)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
            return _mapper.Map<BookmarkDto>(bookmark);
        }

        public async Task<BookmarkDto> FindBookmarkBy(int userId, int assetId)
        {
            var bookmark = await _context.Bookmarks
                .AsNoTracking()
                .Include(b => b.Asset)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .Where(b => b.AssetId == assetId)
                .FirstAsync();
            return _mapper.Map<BookmarkDto>(bookmark);
        }

        public async Task<bool> Exists(int id)
        {
            var bookmark = await _context.Bookmarks
                .AsNoTracking()
                .Include(b => b.Asset)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (bookmark == null)
            {
                return false;
            }
            return true;
        }
    }
}
