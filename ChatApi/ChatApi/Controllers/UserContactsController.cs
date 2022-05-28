using ChatApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.Controllers
{

    [Route("api/[controller]/[action]")]
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
        public async Task<ICollection<UserContact>> IndexAsync()
        {
            return _context.UserContact != null ?
                         await _context.UserContact.ToListAsync() : new List<UserContact> { };
        }

        // POST: UserContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task Create([Bind("UserName,NickName,Server")] UserContact userContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userContact);
                await _context.SaveChangesAsync();
            }
        }

    }
}
