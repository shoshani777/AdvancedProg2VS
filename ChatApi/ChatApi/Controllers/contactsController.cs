using ChatApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

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

        private static string GetUserName(HttpRequest request)
        {
            string token = request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            string userName = new JwtSecurityTokenHandler().ReadJwtToken(token).Payload.Claims.ElementAt(1).Value.ToString();
            return userName;
        }


        // GET: Users
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            string currUserName = GetUserName(Request);
            if (_context.Chat == null || _context.Message == null || _context.UserContact == null)
                return NotFound();
            List<string> toReturn = new List<string>();
            foreach (var chat in await _context.Chat.Where(chat => chat.Name1.Equals(currUserName) ||
                                                            chat.Name2.Equals(currUserName)).ToListAsync())
            {
                if (chat.Name2 == null || chat.Name1 == null)
                    continue;
                string other = currUserName.Equals(chat.Name1) ? chat.Name2 : chat.Name1;
                List<Message> allMessages = await _context.Message.Where(msg => msg.Chat.Equals(chat.Id)).ToListAsync();
                allMessages = allMessages.OrderBy(msg => msg.Created).ToList();
                Message? lastMsg = allMessages.LastOrDefault();
                var otherUser = _context.UserContact.FirstOrDefaultAsync(contact => contact.UserName.Equals(other) &&
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

        public async Task<IActionResult> addContact([FromBodyAttribute][Bind("id,name,server")] ContactForAdding ContactToAdd)
        {
            const string currUserName = "user1";
            if (ModelState.IsValid)
            {
                if (_context.Chat == null || _context.Message == null || _context.UserContact == null)
                    return NotFound();
                if(ContactToAdd.Name ==null || ContactToAdd.Id ==null || ContactToAdd.Server == null)
                    return NotFound();
                bool isContactExist = (await _context.UserContact.FirstOrDefaultAsync(
                contact => currUserName.Equals(contact.ContactOf) && ContactToAdd.Id.Equals(contact.UserName))) != null;
                UserContact newContact = new UserContact { ContactOf = currUserName, Server = ContactToAdd.Server, UserName = ContactToAdd.Id, NickName = ContactToAdd.Name };
                if (!isContactExist)
                {
                    _context.Add(newContact);
                    await _context.SaveChangesAsync();
                }
                bool isChatExist = (await _context.Chat.FirstOrDefaultAsync(
                chat => (currUserName.Equals(chat.Name1) && ContactToAdd.Id.Equals(chat.Name2))
                  || (ContactToAdd.Id.Equals(chat.Name1) && currUserName.Equals(chat.Name2)))) != null;
                Chat newChat = new Chat { Name1 = currUserName, Name2 = ContactToAdd.Id };
                if (!isChatExist)
                {
                    _context.Add(newChat);
                    await _context.SaveChangesAsync();
                }
                //sending invite to "server", composed of: from - currUserName, to - id, server - server

                return CreatedAtAction("addContact", new { id = newChat.Id }, newChat);
            }
            return NotFound();
        }

        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetMessages(string id)
        {
            const string currUserName = "user1";
            if (_context.Chat == null || _context.Message == null)
                return NotFound();
            List<string> toReturn = new List<string>();
            Chat? chat = await _context.Chat.FirstOrDefaultAsync(
                chat => (currUserName.Equals(chat.Name1) && id.Equals(chat.Name2))
                  || (id.Equals(chat.Name1) && currUserName.Equals(chat.Name2)));
            if (chat == null)
                return NotFound();
            int chatId = chat.Id;
            foreach (Message msg in await _context.Message.Where(msg => msg.Chat.Equals(chatId)).ToListAsync())
            {
                if (currUserName.Equals(msg.Author) || id.Equals(msg.Author))
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
        public async Task<IActionResult> NewMessage(string id, [FromBody][Bind("content")] string content)
        {
            const string currUserName = "user1";
            if (_context.Chat == null || _context.UserContact == null)
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
        public async Task<IActionResult> transfer([FromBody][Bind("from,to,content")] Transfer transfer)
        {
            if (_context.Chat == null || transfer.From ==null || transfer.To==null|| transfer.Content == null)
                return NotFound();
            Chat? chat = await _context.Chat.FirstOrDefaultAsync(
                chat => (transfer.To.Equals(chat.Name1) && transfer.From.Equals(chat.Name2))
                  || (transfer.From.Equals(chat.Name1) && transfer.To.Equals(chat.Name2)));
            if (chat == null)
                return NotFound();
            int chatId = chat.Id;
            Message newMsg = new Message { Author = transfer.From, Chat = chatId, Content = transfer.Content, Created = DateTime.Now };
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
            return Ok(new
            {
                id,
                name = chosenContact.NickName,
                server = chosenContact.Server,
                last = lastMsg != null ? lastMsg.Content : null,
                lastdate = lastMsg != null ? lastMsg.Created : null
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(string? id, [FromBody][Bind("NickName,Server")] UserContact contact)
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
            if (id == null || _context.UserContact == null || _context.Chat == null || _context.Message == null)
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
            Chat? chosenChat = chosenChats.FirstOrDefault();
            if (chosenChat == null)
                return NotFound();
            List<Message> contactsMessages = await _context.Message.Where(msg => msg.Chat == chosenChat.Id).ToListAsync();
            _context.Chat.Remove(chosenChat);
            _context.UserContact.Remove(chosenContact);
            foreach (Message message in contactsMessages)
                _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<Message?> GetMessage(string? id, int id2, string myId) // return the correct message after the tests
        {
            if (id == null || _context.Message == null || _context.Chat == null)
                return null;
            Message? chosenMessage = _context.Message.Find(id2);
            if (chosenMessage == null)
                return null;
            List<Chat> chosenChats = await _context.Chat.Where(d => (d.Id == chosenMessage.Chat) && ((d.Name1 == id && d.Name2 == myId) || (d.Name1 == myId && d.Name2 == id))).ToListAsync();
            if (chosenChats == null )
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
            Message? message = GetMessage(id, id2, myId).Result;
            if (message == null)
                return NotFound();
            return Ok(new { id = id2, content = message.Content, created = message.Created.ToString(), sent = message.Author == myId });
        }

        [HttpPut("{id}/[action]/{id2}")]
        public async Task<IActionResult> messages(string? id, int id2, [FromBody][Bind("content")] string content)
        {
            string myId = "user1";
            Message? messageToEdit = GetMessage(id, id2, myId).Result;
            if (messageToEdit == null)
                return NotFound();
            messageToEdit.Content = content;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}/messages/{id2}")]
        public async Task<IActionResult> messagesDelete(string? id, int id2)
        {
            string myId = "user1";
            Message? messageToDelete = GetMessage(id, id2, myId).Result;
            if (messageToDelete == null)
                return NotFound();
            _context.Remove(messageToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> invitations([FromBody][Bind("from,to,server")] Invitation invitation)
        {
            if (_context.UserContact == null || _context.Chat == null || _context.User == null)
                return NotFound();
            if (invitation.From == null || invitation.To == null || invitation.Server == null)
                return NotFound();
            if (_context.User.Find(invitation.To) == null)
                return NotFound();
            UserContact contact = new();
            contact.UserName = invitation.From;
            contact.Server = invitation.Server;
            contact.NickName = invitation.From;
            contact.ContactOf = invitation.To;
            contact.Created = DateTime.Now;

            UserContact? prevUserContact = await _context.UserContact.FirstOrDefaultAsync(
                userContact => invitation.From.Equals(userContact.UserName) && invitation.To.Equals(userContact.ContactOf));
            if(prevUserContact==null)
                _context.UserContact.Add(contact);

            Chat chat = new();
            chat.Name1 = invitation.To;
            chat.Name2 = invitation.From;
            Chat? prevChat = await _context.Chat.FirstOrDefaultAsync(
                chat => (invitation.From.Equals(chat.Name1) && invitation.To.Equals(chat.Name2)) ||
                        (invitation.From.Equals(chat.Name2) && invitation.To.Equals(chat.Name1)));
            if (prevChat == null)
                _context.Chat.Add(chat);
            await _context.SaveChangesAsync();
            return CreatedAtAction("invitations", new { id = chat.Id }, chat);
        }
    }
}