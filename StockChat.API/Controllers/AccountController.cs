using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockChat.API.Helpers;
using StockChat.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockChat.API.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserRepository repo;
        private readonly IConfiguration config;

        public AccountController(IConfiguration configuration, ChatContext context)
        {
            this.config = configuration;
            this.repo = new UserRepository(context);
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<UserDTO> Login([FromBody]UserLoginDTO loginUser)
        {
            var user = this.repo.Get(loginUser.Username);
            if(user == null)
            {
                // Registering a new user
                user = this.repo.Register(loginUser.Username, loginUser.Password);
            }
            else
            {
                user = this.repo.Login(loginUser.Username, loginUser.Password);
            }

            if(user == null)
            {
                return Unauthorized();
            }

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.GetValue<string>("app_secret"));
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handler.CreateToken(descriptor);
            var tokenString = handler.WriteToken(token);

            return Ok(new UserDTO { Username = user.Username, Token = tokenString });
        }
    }
}