using LibraryApp.DTOs;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> Get(int id);
        Task<bool> Add(UserDto newUserDto);
        Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistory(int userId);
        Task<IEnumerable<HoldDto>> GetHolds(int userId);
        Task<IEnumerable<CheckoutDto>> GetCheckouts(int id);
        Task<bool> Register(RegisterRequest request);
        Task<AuthResponse> Login(AuthRequest request);
        Task<bool> EmailExists(string email);
        Task<bool> ResetPassword(ResetPasswordRequest request);
        Task<User> FindByMail(string mailAddress);
        Task<bool> ForgotPassword(string email);
        Task<List<GenericBookDto>> GetCurrentReads(int id);
        Task<List<NotificationDto>> GetLatestNotifications(int userId);
        Task<bool> UpdateNotifications(List<NotificationDto> displayedNotifs);
        Task<int> GetNumberOfUnseenNotifs(int userId);
    }
}
