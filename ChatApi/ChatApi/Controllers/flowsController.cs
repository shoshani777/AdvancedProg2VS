using ChatApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class flowsController : ControllerBase
    {

        private readonly ChatApiContext _context;

        public flowsController(ChatApiContext context)
        {
            _context = context;
        }

        // GET: Users
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            const string currUserName = "user1";
            if (_context.Chat == null || _context.Message == null || _context.UserContact == null)
                return NotFound();
            List<string> toReturn = new List<string>();
            List<Chat> allChats = await _context.Chat.Where(chat => chat.Name1.Equals(currUserName) ||
                                                            chat.Name2.Equals(currUserName)).ToListAsync();
            allChats.OrderBy(chat =>
            {
                string other = currUserName.Equals(chat.Name1) ? chat.Name2 : chat.Name1;
                List<Message> allMessages = _context.Message.Where(msg => msg.Chat.Equals(chat.Id)).ToList();
                allMessages = allMessages.OrderBy(msg => msg.Created).ToList();
                Message? lastMsg = allMessages.LastOrDefault();
                UserContact? otherUser = _context.UserContact.FirstOrDefaultAsync(contact => contact.UserName.Equals(other) &&
                                                                        currUserName.Equals(contact.ContactOf)).Result;
                if (otherUser == null)
                    return DateTime.MinValue;
                return lastMsg != null ? lastMsg.Created : otherUser.Created;
            });
            foreach (Chat chat in allChats)
            {
                if (chat.Name2 == null || chat.Name1 == null)
                    continue;
                string other = currUserName.Equals(chat.Name1) ? chat.Name2 : chat.Name1;
                List<Message> allMessages = await _context.Message.Where(msg => msg.Chat.Equals(chat.Id)).ToListAsync();
                allMessages = allMessages.OrderBy(msg => msg.Created).ToList();
                Message? lastMsg = allMessages.LastOrDefault();
                UserContact? otherUser = _context.UserContact.FirstOrDefaultAsync(contact => contact.UserName.Equals(other) &&
                                                                        currUserName.Equals(contact.ContactOf)).Result;
                if (otherUser == null)
                    continue;
                string current = JsonConvert.SerializeObject(new
                {
                    id = otherUser.UserName,
                    name = otherUser.NickName,
                    server = otherUser.Server,
                    last = lastMsg != null ? lastMsg.Content : null,
                    lastdate = lastMsg != null ? lastMsg.Created : otherUser.Created,
                    messages = allMessages,
                    isClicked = otherUser.isClicked,
                    unread = otherUser.unread,
                    unreadMark = otherUser.unreadMark
                });
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
        }


        [HttpPut("contact/{id}")]
        public async Task<IActionResult> EditAsync(string? id, [FromBody][Bind("unread,unreadMark,isClicked")] ContactForEditting contact)
        {
            string myId = "user1";
            if (id == null || _context.UserContact == null)
                return NotFound();
            var chosenContacts = _context.UserContact.Where(d => d.UserName == id && d.ContactOf == myId);
            if (chosenContacts == null || !chosenContacts.Any())
                return NotFound();
            var chosenContact = chosenContacts.FirstOrDefault();
            if (chosenContact == null)
                return NotFound();
            if (contact.isClicked != null)
                chosenContact.isClicked = (bool)contact.isClicked;
            if (contact.unread != null)
                chosenContact.unread = (int)contact.unread;
            if (contact.unreadMark != null)
                chosenContact.unreadMark = (int)contact.unreadMark;
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }        
}