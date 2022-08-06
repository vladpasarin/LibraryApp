using LibraryApp.DTOs;
using LibraryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<bool> Add(UserDto newUserDto);
        public Task<UserDto> GetById(int id);
        public Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistoryByUserId(int id);
        public Task<IEnumerable<CheckoutDto>> GetCheckouts(int id);
        public Task<IEnumerable<HoldDto>> GetHolds(int id);
        public Task<UserDto> Register(User user, string password);
        public Task<User> Login(string email, string password);
        public Task<bool> EmailExists(string email);
        public Task<User> FindByMail(string mailAddress);
        public Task<bool> ResetPassword(string email, string newPassword, string token);
    }
}
