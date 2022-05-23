using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatApi;
using ChatApi.Data;

namespace ChatApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserContactsController : ControllerBase
    {
        private readonly ChatApiContext _context;

        public UserContactsController(ChatApiContext context)
        {
            _context = context;
        }

        // GET: UserContacts
        [HttpGet]
        public async Task<ICollection<UserContact>> Index()
        {
            if (_context.UserContact == null)
            {
                return new List<UserContact> { };
            }
            return await _context.UserContact.ToListAsync();
        }

        // GET: UserContacts/5
        [HttpGet("{id}")]
        public async Task<UserContact> Details(string id)
        {
            if (id == null || _context.UserContact == null)
            {
                return new UserContact();
            }

            var userContact = await _context.UserContact
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (userContact == null)
            {
                return new UserContact();
            }

            return userContact;
        }



        // POST: UserContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [IgnoreAntiforgeryToken]


        // [ValidateAntiForgeryToken]
        public async Task Create([Bind("Id")] UserContact userContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userContact);
                await _context.SaveChangesAsync();
            }
        }
    }
}
