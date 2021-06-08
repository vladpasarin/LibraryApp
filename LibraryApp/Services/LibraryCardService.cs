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
    public class LibraryCardService : ILibraryCardService
    {
        private readonly ILibraryCardRepository _repo;
        private readonly IMapper _mapper;

        public LibraryCardService(ILibraryCardRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> Add(LibraryCardDto newLibraryCard)
        {
            var newCard = _mapper.Map<LibraryCard>(newLibraryCard);
            await _repo.Create(newCard);
            return await _repo.SaveChanges();
        }

        public async Task<LibraryCardDto> Get(int cardId)
        {
            return await _repo.Get(cardId);
        }

        public async Task<IEnumerable<LibraryCardDto>> GetAllLibraryCards()
        {
            return await _repo.GetAllLibraryCards();
        }
    }
}
