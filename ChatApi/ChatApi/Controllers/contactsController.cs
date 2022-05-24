using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatApi;
using ChatApi.Data;
using Newtonsoft.Json;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class contactsController : ControllerBase
    {

        private readonly ChatApiContext _context;

        public contactsController(ChatApiContext context)
        {
            _context = context;
        }


        // GET: Users
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            const string currUserName = "user1";
            if(_context.Chat==null)
                return Problem("Entity set 'ChatAPI.Chat'  is null.");
            List<string> toReturn = new List<string>();
            foreach (var chat in await _context.Chat.Where(chat=> chat.Name1.Equals(currUserName)||
                                                            chat.Name2.Equals(currUserName)).ToListAsync())
            {
                string other = chat.Name1.Equals(currUserName) ? chat.Name2 : chat.Name1;
                var otherUser = _context.UserContact.FirstOrDefaultAsync(user=>user.UserName.Equals(other)).Result;
                string current = JsonConvert.SerializeObject(new { id = otherUser.UserName,
                                                                    server = otherUser.Server
                                                                });//name = ...,last = ..., lastdate = ...
                toReturn.Add(current);
            }
            string final = "[";
            foreach (var item in toReturn)
            {
                final += item;
                if (!toReturn.Last().Equals(item))
                    final += ",";
            }
            final += "]";
            return Ok(final);
            //return _context.User != null ?
            //             await _context.User.ToListAsync() : new List<User> { };
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
