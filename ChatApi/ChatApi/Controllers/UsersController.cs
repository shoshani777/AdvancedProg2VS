using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatApi;
using ChatApi.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly ChatApiContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(ChatApiContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        private JwtSecurityToken GetToken(string userName)
        {
            var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("UserName", userName)
                    };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: mac
            );
            return token;
        }

        // GET: Users
        [HttpGet]
        public async Task<ICollection<User>> IndexAsync()
        {
            return _context.User != null ?
                         await _context.User.ToListAsync() : new List<User> { };
        }

        [ActionName("register")]
        [HttpPost]
        public async Task<IActionResult> Register([Bind("UserName,Password,NickName")] User user)
        {
            if (ModelState.IsValid)
            {
                var q = from u in _context.User
                        where u.UserName == user.UserName
                        select u;
                if (q.Any())
                {
                    return BadRequest("username already exist");
                }
                _context.Add(user);
                await _context.SaveChangesAsync();
                return Ok(new JwtSecurityTokenHandler().WriteToken(GetToken(user.UserName)));
            }
            return BadRequest();
        }

        [ActionName("login")]
        [HttpPost]
        public IActionResult Login([Bind("UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var q = from u in _context.User
                        where u.UserName == user.UserName && u.Password == user.Password
                        select u;
                if (q.Any())
                {
                    return Ok(new JwtSecurityTokenHandler().WriteToken(GetToken(user.UserName)));
                }
                else
                {
                    return BadRequest("incorrect username/password");
                }
            }
            return BadRequest();
        }
    }
}
