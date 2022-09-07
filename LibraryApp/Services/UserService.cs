using AutoMapper;
using EmailService;
using LibraryApp.DTOs;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using LibraryApp.IServices;
using LibraryApp.Models;
using LibraryApp.Repositories.IRepositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly ILibraryCardService _lcService;
        private readonly IEmailSender _emailSender;
        private readonly IBookService _bookService;
        private readonly INotificationRepository _notifRepo;

        public UserService(IUserRepository userRepo, IOptions<AppSettings> options, IMapper mapper, ILibraryCardService lcService, 
            IEmailSender emailSender, IBookService bookService, INotificationRepository notifRepo)
        {
            _userRepo = userRepo;
            _appSettings = options.Value;
            _mapper = mapper;
            _lcService = lcService;
            _emailSender = emailSender;
            _bookService = bookService;
            _notifRepo = notifRepo;
        }

        public async Task<bool> Add(UserDto newUserDto)
        {
            await _userRepo.Add(newUserDto);
            return await _userRepo.SaveChanges();
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _userRepo.EmailExists(email);
        }

        public async Task<User> FindByMail(string mailAddress)
        {
            return await _userRepo.FindByMail(mailAddress);
        }

        public async Task<bool> ForgotPassword(string email)
        {
            try
            {
                var user = await FindByMail(email);
                if (user == null)
                {
                    throw new ApplicationException("User not found");
                }

                var resetToken = GenerateJwtForUser(user);

                user.PasswordResetToken = resetToken;
                await _userRepo.SaveChanges();

                var message = new Message(new string[] { "vladpasarin@yahoo.com" }, "Password reset for LibraryApp", String.Format("Password reset token for LibraryApp<br>" +
                    "<br>Here is your token: <b>{0}</b><br><br>Yours truly, <br>LibraryApp Team", resetToken));

                await _emailSender.SendEmailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<UserDto> Get(int id)
        {
            return await _userRepo.GetById(id);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var registeredUsers = await _userRepo.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(registeredUsers);
        }

        public async Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistory(int userId)
        {
            return await _userRepo.GetCheckoutHistoryByUserId(userId);
        }

        public async Task<IEnumerable<CheckoutDto>> GetCheckouts(int id)
        {
            return await _userRepo.GetCheckouts(id);
        }

        public async Task<List<GenericBookDto>> GetCurrentReads(int id)
        {
            var history = await GetCheckoutHistory(id);

            var currentlyReading = history.Where(h => h.CheckedIn == null).ToList();
            if (currentlyReading.Count == 0)
            {
                return null;
            }
            
            List<AssetDto> assets = new List<AssetDto>();
            foreach (var reading in currentlyReading)
            {
                assets.Add(reading.Asset);
            }

            List<GenericBookDto> genericBooks = new List<GenericBookDto>();
            foreach (var asset in assets)
            {
                genericBooks.Add(await _bookService.GetGenericBook(asset.Id));
            }

            return genericBooks;
        }

        public async Task<IEnumerable<HoldDto>> GetHolds(int userId)
        {
            return await _userRepo.GetHolds(userId);
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {

            var user = await _userRepo.Login(request.Email, request.Password);
            if (user == null)
                return null;

            var token = GenerateJwtForUser(user);

            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var libraryCard = new LibraryCard
            {
                CurrentFees = 0.0,
                DateIssued = DateTime.Now
            };
            var entity = new User
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                //Username = request.Username,
                Address = request.Address,
                PhoneNr = request.PhoneNr,
                DateOfBirth = request.DateOfBirth,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                LibraryCard = libraryCard,
                LibraryCardId = libraryCard.Id
            };

            await _userRepo.Create(entity);
            await _userRepo.Register(entity, entity.Password);
            return await _userRepo.SaveChanges();
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            return await _userRepo.ResetPassword(request.Email, request.Password, request.Token);
        }

        public async Task<bool> UpdateNotifications(List<NotificationDto> displayedNotifs)
        {
            return await _notifRepo.UpdateNotifications(displayedNotifs);
        }

        public async Task<List<NotificationDto>> GetLatestNotifications(int userId)
        {
            return await _notifRepo.GetLatestNotifications(userId);
        }

        public async Task<int> GetNumberOfUnseenNotifs(int userId)
        {
            return await _notifRepo.GetNumberOfUnseenNotifs(userId);
        }

        private string GenerateJwtForUser(User user)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
