using AutoMapper;
using LibraryApp.Data;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Users
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly IMapper _mapper;
        public NotificationRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(string message, int userId)
        {
            var notif = new Notification
            {
                Message = message,
                DateCreated = System.DateTime.UtcNow,
                UserId = userId,
            };
            await Create(notif);
            return await SaveChanges();
        }

        public async Task<List<NotificationDto>> GetLatestNotifications(int userId)
        {
            var notifs = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.DateCreated)
                .Take(5)
                .ToListAsync();

            return _mapper.Map<List<NotificationDto>>(notifs);
        }

        public async Task<int> GetNumberOfUnseenNotifs(int userId)
        {
            var notifs = await _context.Notifications
                .Where(n => n.UserId == userId && n.Seen == false)
                .ToListAsync();

            return notifs.Count;
        }

        public async Task<bool> UpdateNotifications(List<NotificationDto> displayedNotifs)
        {
            foreach (var notifDto in displayedNotifs)
            {
                var notif = _mapper.Map<Notification>(notifDto);
                notif.Seen = true;
                Update(notif);
            }

            return await SaveChanges();
        }
    }
}
