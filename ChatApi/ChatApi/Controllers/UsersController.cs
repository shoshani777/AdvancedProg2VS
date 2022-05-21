using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatApi;
using ChatApi.Data;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ChatApiContext _context;

        public UsersController(ChatApiContext context)
        {
            _context = context;
        }

        // GET: Users
        [HttpGet]
        public async Task<ICollection<User>> IndexAsync()
        {
            return _context.User != null ?
                         await _context.User.ToListAsync() : new List<User> { };
        }

        // GET: Users/Details/5
        [HttpGet("{id}")]
        public async Task<User> Details(string id)
        {
            if (id == null || _context.User == null)
            {
                return new User();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (user == null)
            {
                return new User();
            }

            return user;
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        [IgnoreAntiforgeryToken]


        public async Task Create([Bind("UserName,Password,NickName,Server")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
