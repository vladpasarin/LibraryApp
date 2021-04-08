using LibraryApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.IServices;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate del, IOptions<AppSettings> options)
        {
            _next = del;
            _appSettings = options.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContextByToken(context, userService, token);

            await _next(context);
        }

        private void AttachUserToContextByToken(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken securityToken);

                var jwtToken = (JwtSecurityToken)securityToken;
                var userId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "id").Value);

                context.Items["User"] = userService.Get(userId);
            }
            catch (Exception)
            {

            }
        }
    }
}
