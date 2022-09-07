using AutoMapper;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities.Assets.Tags;
using LibraryApp.IServices;
using LibraryApp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class TagService : ITagService
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _repo;

        public TagService(ITagRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<bool> Add(TagDto newTagDto)
        {
            var tag = _mapper.Map<Tag>(newTagDto);
            await _repo.Create(tag);
            return await _repo.SaveChanges();
        }

        public async Task<TagDto> Get(int id)
        {
            var tag = await _repo.FindById(id);
            return _mapper.Map<TagDto>(tag);
        }

        public async Task<IEnumerable<TagDto>> GetAll()
        {
            var tags = await _repo.GetAll();
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }
    }
}
