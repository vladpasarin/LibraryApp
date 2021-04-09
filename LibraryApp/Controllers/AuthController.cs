﻿using LibraryApp.DTOs;
using LibraryApp.IServices;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController(IUserService service)
        {
            _service = service;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var user = (UserDto)HttpContext.Items["User"];
            var users = await _service.GetAll();
            if (users == null)
                return StatusCode(500);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = (UserDto)HttpContext.Items["User"];
            var users = await _service.Get(id);
            if (users == null)
                return StatusCode(500);

            return Ok(users);
        }
    }
}
