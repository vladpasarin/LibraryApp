using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using LibraryApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryApp.DTOs;
using AutoMapper;

namespace LibraryApp.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> Add(UserDto newUserDto)
        {
            var newUser = _mapper.Map<User>(newUserDto);
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _context.Users
                .Include(a => a.LibraryCard)
                .FirstOrDefaultAsync(p => p.Id == id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistoryByUserId(int id)
        {
            var user = await _context.Users
                .Include(x => x.LibraryCard)
                .FirstAsync(a => a.Id == id);

            if (user == null)
                return null;

            var cardId = user.LibraryCard.Id;

            var history = _context.CheckoutHistories
                .Include(x => x.LibraryCard)
                .Include(x => x.Asset)
                .Where(x => x.LibraryCard.Id == cardId)
                .OrderByDescending(x => x.CheckedOut);

            return _mapper.Map<IEnumerable<CheckoutHistoryDto>>(history);
        }

        public async Task<IEnumerable<CheckoutDto>> GetCheckouts(int id)
        {
            var user = await _context.Users
                .Include(x => x.LibraryCard)
                .FirstAsync(x => x.Id == id);

            if (user == null)
                return null;

            var cardId = user.LibraryCard.Id;

            var checkouts = _context.Checkouts
                .Include(x => x.LibraryCard)
                .Include(x => x.Asset)
                .Where(x => x.LibraryCard.Id == cardId)
                .OrderByDescending(x => x.CheckedOutSince);

            return _mapper.Map<IEnumerable<CheckoutDto>>(checkouts);
        }

        public async Task<IEnumerable<HoldDto>> GetHolds(int id)
        {
            var user = await _context.Users
                .Include(x => x.LibraryCard)
                .FirstAsync(x => x.Id == id);

            if (user == null)
                return null;

            var cardId = user.LibraryCard.Id;

            var holds = _context.Holds
                .Include(x => x.LibraryCard)
                .Include(x => x.Asset)
                .Where(x => x.LibraryCard.Id == cardId)
                .OrderByDescending(x => x.HoldPlaced);

            return _mapper.Map<IEnumerable<HoldDto>>(holds);
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<UserDto> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> EmailExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email == email))
            {
                return true;
            }
            return false;
        }
    }
}
