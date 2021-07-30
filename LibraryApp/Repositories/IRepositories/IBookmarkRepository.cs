using LibraryApp.DTOs;
using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IBookmarkRepository : IGenericRepository<Bookmark>
    {
        public Task<BookmarkDto> Get(int id);
    }
}
