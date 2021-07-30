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
                .Include(b => b.Asset)
                .Include(b => b.User)
                .FirstAsync(b => b.Id == id);
            return _mapper.Map<BookmarkDto>(bookmark);
        }
    }
}
