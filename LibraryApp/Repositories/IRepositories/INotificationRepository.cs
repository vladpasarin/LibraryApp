using LibraryApp.DTOs;
using LibraryApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        public Task<bool> CreateAsync(string message, int userId);
        public Task<List<NotificationDto>> GetLatestNotifications(int userId);
        public Task<bool> UpdateNotifications(List<NotificationDto> displayedNotifs);
        public Task<int> GetNumberOfUnseenNotifs(int userId);
    }
}
