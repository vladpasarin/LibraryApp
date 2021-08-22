using LibraryApp.Data;
using LibraryApp.Entities.Assets.Tags;
using LibraryApp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Assets
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(LibraryDbContext context) : base(context)
        {

        }
    }
}
