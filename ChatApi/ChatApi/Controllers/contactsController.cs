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
            if(_context.Chat==null || _context.Message==null || _context.UserContact == null)
                return NotFound();
            List<string> toReturn = new List<string>();
            foreach (var chat in await _context.Chat.Where(chat=> chat.Name1.Equals(currUserName)||
                                                            chat.Name2.Equals(currUserName)).ToListAsync())
            {
                if (chat.Name2 == null || chat.Name1 == null)
                    continue;
                string other = currUserName.Equals(chat.Name1) ? chat.Name2 : chat.Name1;
                List<Message> allMessages = await _context.Message.Where(msg => msg.Chat.Equals(chat.Id)).ToListAsync();
                allMessages = allMessages.OrderBy(msg => msg.Created).ToList();
                Message? lastMsg = allMessages.LastOrDefault();
                var otherUser = _context.UserContact.FirstOrDefaultAsync(contact=> contact.UserName.Equals(other)&&
                                                                        currUserName.Equals(contact.ContactOf)).Result;
                if (otherUser == null)
                    continue;
                string current = JsonConvert.SerializeObject(new
                {
                    id = otherUser.UserName,
                    name = otherUser.NickName,
                    server = otherUser.Server,
                    last = lastMsg != null ? lastMsg.Content : null,
                    lastdate = lastMsg != null ? lastMsg.Created : null
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

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        [IgnoreAntiforgeryToken]


        public async Task<IActionResult> addUser([Bind("id")] string id,[Bind("name")] string name,[Bind("server")] string server)
        {
            const string currUserName = "user1";
            if (ModelState.IsValid)
            {
                if (_context.Chat == null || _context.Message == null || _context.UserContact == null)
                    return NotFound();
                bool isContactExist = (await _context.UserContact.FirstOrDefaultAsync(
                contact => currUserName.Equals(contact.ContactOf) && id.Equals(contact.UserName))) != null;
                UserContact newContact = new UserContact { ContactOf = currUserName, Server = server, UserName = id, NickName = name };
                if (!isContactExist)
                {
                    _context.Add(newContact);
                    await _context.SaveChangesAsync();
                }
                bool isChatExist = (await _context.Chat.FirstOrDefaultAsync(
                chat => (currUserName.Equals(chat.Name1) && id.Equals(chat.Name2))
                  || (id.Equals(chat.Name1) && currUserName.Equals(chat.Name2)))) != null;
                Chat newChat = new Chat { Name1 = currUserName, Name2 = id };
                if (!isChatExist)
                {
                    _context.Add(newChat);
                    await _context.SaveChangesAsync();
                }
                //sending invite to "server", composed of: from - currUserName, to - id, server - server

                return CreatedAtAction("addUser", new { id = newChat.Id }, newChat);
            }
            return NotFound();
        }

        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetMessages(string id)
        {
            const string currUserName = "user1";
            if (_context.Chat == null || _context.Message==null)
                return NotFound();
            List<string> toReturn = new List<string>();
            Chat? chat = await _context.Chat.FirstOrDefaultAsync(
                chat => (currUserName.Equals(chat.Name1) && id.Equals(chat.Name2))
                  || (id.Equals(chat.Name1) && currUserName.Equals(chat.Name2)));
            if(chat==null)
                return NotFound();
            int chatId = chat.Id;
            foreach (Message msg in await _context.Message.Where(msg => msg.Chat.Equals(chatId)).ToListAsync())
            {
                if(currUserName.Equals(msg.Author)|| id.Equals(msg.Author))
                {
                    string current = JsonConvert.SerializeObject(new
                    {
                        id = msg.Id,
                        content = msg.Content,
                        Created = msg.Created,
                        sent = msg.Author.Equals(currUserName)
                    });
                    toReturn.Add(current);
                }
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

        [HttpPost("{id}/messages")]
        public async Task<IActionResult> NewMessage(string id, [Bind("content")] string content)
        {
            const string currUserName = "user1";
            if (_context.Chat == null || _context.UserContact==null)
                return NotFound();
            Chat? chat = await _context.Chat.FirstOrDefaultAsync(
                chat => (currUserName.Equals(chat.Name1) && id.Equals(chat.Name2))
                  || (id.Equals(chat.Name1) && currUserName.Equals(chat.Name2)));
            if (chat == null)
                return NotFound();
            int chatId = chat.Id;
            Message newMsg = new Message { Author = currUserName, Chat = chatId, Content = content, Created = DateTime.Now };
            _context.Add(newMsg);
            await _context.SaveChangesAsync();
            //calling transfer to get the message to the other user - id - located at server
            //composed of: from - currUserName, to - id, content - content
            return CreatedAtAction("NewMessage", new { id = newMsg.Id }, newMsg);
        }
        [HttpPost("transfer")]
        public async Task<IActionResult> transfer([Bind("from")] string from, [Bind("to")] string to, [Bind("content")] string content )
        {
            if (_context.Chat == null)
                return NotFound();
            Chat? chat = await _context.Chat.FirstOrDefaultAsync(
                chat => (to.Equals(chat.Name1) && from.Equals(chat.Name2))
                  || (from.Equals(chat.Name1) && to.Equals(chat.Name2)));
            if (chat == null)
                return NotFound();
            int chatId = chat.Id;
            Message newMsg = new Message { Author = from, Chat = chatId, Content = content, Created = DateTime.Now };
            _context.Add(newMsg);
            await _context.SaveChangesAsync();
            return CreatedAtAction("NewMessage", new { id = newMsg.Id }, newMsg);
        }


        //gilad

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string? id)
        {
            string myId = "user1";
            if (id == null || _context.UserContact == null || _context.Chat == null || _context.Message == null)
            {
                return NotFound();
            }


            var chosenChats = _context.Chat.Where(d => (d.Name1 == id && d.Name2 == myId) || (d.Name1 == myId && d.Name2 == id));
            if (chosenChats == null || !chosenChats.Any())
                return NotFound();
            var chosenChat = chosenChats.FirstOrDefault();
            if (chosenChat == null)
                return NotFound();
            var chosenContacts = _context.UserContact.Where(d => d.UserName == id && d.ContactOf == myId);
            if (chosenContacts == null || !chosenContacts.Any())
                return NotFound();
            var chosenContact = chosenContacts.FirstOrDefault();
            if (chosenContact == null)
                return NotFound();

            List<Message> allMessages = await _context.Message.Where(msg => msg.Chat.Equals(chosenChat.Id)).ToListAsync();
            allMessages = allMessages.OrderBy(msg => msg.Created).ToList();
            Message? lastMsg = allMessages.LastOrDefault();
            return Ok(new { id, 
                name = chosenContact.NickName,
                server = chosenContact.Server,
                last = lastMsg != null ? lastMsg.Content : null,
                lastdate = lastMsg != null ? lastMsg.Created : null
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(string? id, [Bind("NickName,Server")] UserContact contact)
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
            chosenContact.NickName = contact.NickName;
            chosenContact.Server = contact.Server;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            string myId = "user1";
            if (id == null || _context.UserContact == null || _context.Chat == null)
                return NotFound();

            var chosenContacts = _context.UserContact.Where(d => d.UserName == id && d.ContactOf == myId);
            if (chosenContacts == null || !chosenContacts.Any())
                return NotFound();
            var chosenContact = chosenContacts.FirstOrDefault();
            if (chosenContact == null)
                return NotFound();
            var chosenChats = _context.Chat.Where(d => (d.Name1 == id && d.Name2 == myId) || (d.Name1 == myId && d.Name2 == id));
            if (chosenChats == null || !chosenChats.Any())
                return NotFound();
            var chosenChat = chosenChats.FirstOrDefault();
            if (chosenChat == null)
                return NotFound();
            _context.Chat.Remove(chosenChat);
            _context.UserContact.Remove(chosenContact);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private Message? GetMessage(string? id, int id2, string myId) // return the correct message after the tests
        {
            if (id == null || _context.Message == null || _context.Chat == null)
                return null;
            Message? chosenMessage = _context.Message.Find(id2);
            if (chosenMessage == null)
                return null;
            var chosenChats = _context.Chat.Where(d => d.Id == chosenMessage.Chat && (d.Name1 == id && d.Name2 == myId) || (d.Name1 == myId && d.Name2 == id));
            if (chosenChats == null || !chosenChats.Any())
                return null;
            var chosenChat = chosenChats.FirstOrDefault();
            if (chosenChat == null)
                return null;
            return chosenMessage;
        }
        [HttpGet("{id}/[action]/{id2}")]
        public IActionResult messages(string? id, int id2)
        {
            string myId = "user1";
            Message? message = GetMessage(id, id2, myId);
            if (message == null)
                return NotFound();
            return Ok(new { id = id2, content = message.Content, created = message.Created.ToString(), sent = message.Author == myId });
        }

        [HttpPut("{id}/[action]/{id2}")]
        public async Task<IActionResult> messages(string? id, int id2, [Bind("content")] Message message)
        {
            string myId = "user1";
            Message? messageToEdit = GetMessage(id, id2, myId);
            if (messageToEdit == null)
                return NotFound();
            messageToEdit.Content = message.Content;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}/messages/{id2}")]
        public async Task<IActionResult> messagesDelete(string? id, int id2)
        {
            string myId = "string";
            Message? messageToDelete = GetMessage(id, id2, myId);
            if (messageToDelete == null)
                return NotFound();
            _context.Remove(messageToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> invitations([Bind("from,to,server")] Invitation invitation)
        {
            if (_context.UserContact == null || _context.Chat == null || _context.User == null)
                return NotFound();
            if (_context.User.Find(invitation.To) == null)
                return NotFound();
            UserContact contact = new();
            contact.UserName = invitation.From;
            contact.Server = invitation.Server;
            contact.NickName = invitation.From;
            contact.ContactOf = invitation.To;
            _context.UserContact.Add(contact);

            Chat chat = new();
            chat.Name1 = invitation.To;
            chat.Name2 = invitation.From;
            _context.Chat.Add(chat);
            await _context.SaveChangesAsync();
            return CreatedAtAction("invitations", new { id = chat.Id }, chat);
        }

    }
}
