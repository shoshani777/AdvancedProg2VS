using ChatApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
                if (_context.User == null || user.Password == null)
                    return NotFound();
                string afterHash = "";
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(user.Password);
                    afterHash = Encoding.ASCII.GetString(mySHA256.ComputeHash(bytes));
                }
                List<User> users = _context.User.Where(d => d.UserName == user.UserName).ToList();                
                if (users.Count>0)
                    return NotFound("username already exist");
                if (user.UserName == null)
                    return NotFound();
                user.Password = afterHash;
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
                if (_context.User == null || user.Password == null)
                    return NotFound();
                string afterHash = "";
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(user.Password);
                    afterHash = Encoding.ASCII.GetString(mySHA256.ComputeHash(bytes));
                }
                List<User> users = _context.User.Where(d => d.UserName == user.UserName && d.Password == afterHash).ToList();
                if (users.Count>0)
                {
                    if(user.UserName!=null)
                        return Ok(new JwtSecurityTokenHandler().WriteToken(GetToken(user.UserName)));
                    return NotFound();
                }
                return NotFound("incorrect username/password");
            }
            return BadRequest();
        }
    }
}
