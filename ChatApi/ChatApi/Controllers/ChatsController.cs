using ChatApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly ChatApiContext _context;

        public ChatsController(ChatApiContext context)
        {
            _context = context;
        }

        // GET: Chats
        [HttpGet]
        public async Task<ICollection<Chat>> Index()
        {
            return _context.Chat != null ?
                        await _context.Chat.ToListAsync() :
                        new List<Chat> { };
        }

        // GET: Chats/Details/5
        [HttpGet("{id}")]
        public async Task<Chat> Details(int? id)
        {
            if (id == null || _context.Chat == null)
            {
                return new Chat();
            }

            var chat = await _context.Chat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chat == null)
            {
                return new Chat();
            }

            return chat;
        }

        // POST: Chats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [IgnoreAntiforgeryToken]


        // [ValidateAntiForgeryToken]
        public async Task Create([Bind("Id")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chat);
                await _context.SaveChangesAsync();
            }
        }

    }
}