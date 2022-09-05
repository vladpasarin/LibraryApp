using EmailService;
using LibraryApp.DTOs;
using LibraryApp.Entities;
using LibraryApp.IServices;
using LibraryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IUserService _service;
        public readonly IEmailSender _emailSender;
        //private readonly UserManager<User> _userManager;

        public AuthController(IUserService service, IEmailSender emailSender)
        {
            _service = service;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // TODO: validate request
            if (await _service.EmailExists(request.Email))
                return BadRequest("Email already exists in our system");

            return Ok(await _service.Register(request));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            return Ok(await _service.Login(request));
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var user = (UserDto)HttpContext.Items["User"];
            var users = await _service.GetAll();
            if (users == null)
                return StatusCode(500);

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = (UserDto)HttpContext.Items["User"];
            var users = await _service.Get(id);
            if (users == null)
                return StatusCode(500);

            return Ok(users);
        }

        [HttpPut("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            return Ok(await _service.ResetPassword(request));
        }

        [HttpGet("sendMail")]
        public async Task SendMail()
        {
            var message = new Message(new string[] { "vladpasarin@yahoo.com" }, "Test email", "This is the content from our email.");
            await _emailSender.SendEmailAsync(message);
        }

        [Authorize]
        [HttpGet("history/{id}")]
        public async Task<IActionResult> GetNumberOfReadBooks(int id)
        {
            var readBooks = await _service.GetCheckoutHistory(id);
            if (readBooks == null)
            {
                return Ok(0);
            }
            return Ok(readBooks.Count());
        }

        [Authorize]
        [HttpGet("holds/{id}")]
        public async Task<IActionResult> GetNumberOfHolds(int id)
        {
            var holds = await _service.GetHolds(id);
            if (holds == null)
            {
                return Ok(0);
            }
            return Ok(holds.Count());
        }

        [Authorize]
        [HttpGet("currentRead/{id}")]
        public async Task<IActionResult> GetCurrentRead(int id)
        {
            var genericBooks = await _service.GetCurrentReads(id);
            if (genericBooks == null)
                return null;
            return Ok(genericBooks);
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            return Ok(await _service.ForgotPassword(forgotPasswordDto.Email));
        }

        [Authorize]
        [HttpGet("latestNotifications")]
        public async Task<IActionResult> GetLatestNotifications(int userId)
        {
            var notifs = await _service.GetLatestNotifications(userId);
            if (notifs == null)
            {
                return NoContent();
            }
            return Ok(notifs);
        }

        [Authorize]
        [HttpGet("newNotifications")]
        public async Task<IActionResult> GetNumberOfUnseenNotifs(int userId)
        {
            var newNotifs = await _service.GetNumberOfUnseenNotifs(userId);
            return Ok(newNotifs);
        }

        [Authorize]
        [HttpPut("updateNotifs")]
        public async Task<IActionResult> UpdateNotifs(List<NotificationDto> displayedNotifs)
        {
            return Ok(await _service.UpdateNotifications(displayedNotifs));
        }

        /*
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var emailExists = await _service.EmailExists(forgotPasswordDto.Email);
            if (emailExists == false)
                return BadRequest("User with provided email not found");

            var userFound = await _service.FindByMail(forgotPasswordDto.Email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(userFound);
            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", forgotPasswordDto.Email}
            };

            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);
            var message = new Message(new string[] { userFound.Email }, "Reset password token", callback);

            await _emailSender.SendEmailAsync(message);

            return Ok();
        }*/
    }
}
