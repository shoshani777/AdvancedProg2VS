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
using Microsoft.AspNetCore.Authorization;
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
            const string currUserName = "string";
            if (_context.Chat == null)
                return Problem("Entity set 'ChatAPI.Chat'  is null.");
            List<string> toReturn = new List<string>();
            foreach (var chat in await _context.Chat.Where(chat => chat.Name1.Equals(currUserName) ||
                                                            chat.Name2.Equals(currUserName)).ToListAsync())
            {
                string other = chat.Name1.Equals(currUserName) ? chat.Name2 : chat.Name1;
                var otherUser = _context.UserContact.FirstOrDefaultAsync(user => user.UserName.Equals(other)).Result;
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

        public async Task Create([Bind("UserName,ContactOf,NickName,Server")] UserContact contact)
        {
            string myId = "string";
            if (ModelState.IsValid)
            {
                _context.UserContact.Add(contact);
                Chat chat = new();
                chat.Name1 = myId;
                chat.Name2 = contact.UserName;
                _context.Chat.Add(chat);
                await _context.SaveChangesAsync();
            }
        }


        [HttpGet("{id}")]
        public IActionResult Details(string? id)
        {
            string myId = "string";
            if (id == null || _context.UserContact == null || _context.Chat == null)
            {
                return BadRequest();
            }


            var chosenChats = _context.Chat.Where(d => (d.Name1 == id && d.Name2 == myId) || (d.Name1 == myId && d.Name2 == id));
            if (chosenChats == null || !chosenChats.Any())
            {
                return BadRequest("contact does not exist");
            }
            var chosenChat = chosenChats.FirstOrDefault();
            if (chosenChat == null)
            {
                return BadRequest("contact does not exist");
            }
            var chosenContacts = _context.UserContact.Where(d => d.UserName == id && d.ContactOf == myId);
            if (chosenContacts == null || !chosenContacts.Any())
            {
                return BadRequest("contact does not exist");
            }
            var chosenContact = chosenContacts.FirstOrDefault();
            if (chosenContact == null)
            {
                return BadRequest("contact does not exist");
            }

            if (chosenChat.Messages == null)
            {
                return Ok(new { id, name = chosenContact.NickName, server = chosenContact.Server });
            }
            var last = chosenChat.Messages.Last();
            return Ok(new { id, name = chosenContact.NickName, server = chosenContact.Server, last = last.Content, lastdate = last.Created });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(string? id, [Bind("NickName,Server")] UserContact contact)
        {
            string myId = "string";
            if (id == null || _context.UserContact == null)
            {
                return BadRequest();
            }
            var chosenContacts = _context.UserContact.Where(d => d.UserName == id && d.ContactOf == myId);
            if (chosenContacts == null || !chosenContacts.Any())
            {
                return BadRequest("contact does not exist");
            }
            var chosenContact = chosenContacts.FirstOrDefault();
            if (chosenContact == null)
            {
                return BadRequest("contact does not exist");
            }
            chosenContact.NickName = contact.NickName;
            chosenContact.Server = contact.Server;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            string myId = "string";
            if (id == null || _context.UserContact == null || _context.Chat == null)
            {
                return BadRequest();
            }

            var chosenContacts = _context.UserContact.Where(d => d.UserName == id && d.ContactOf == myId);
            if (chosenContacts == null || !chosenContacts.Any())
            {
                return BadRequest("contact does not exist");
            }
            var chosenContact = chosenContacts.FirstOrDefault();
            if (chosenContact == null)
            {
                return BadRequest("contact does not exist");
            }
            var chosenChats = _context.Chat.Where(d => (d.Name1 == id && d.Name2 == myId) || (d.Name1 == myId && d.Name2 == id));
            if (chosenChats == null || !chosenChats.Any())
            {
                return BadRequest("contact does not exist");
            }
            var chosenChat = chosenChats.FirstOrDefault();
            if (chosenChat == null)
            {
                return BadRequest("contact does not exist");
            }
            _context.Chat.Remove(chosenChat);
            _context.UserContact.Remove(chosenContact);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private Message? GetMessage(string? id, int id2, string myId) // return the correct message after the tests
        {
            if (id == null || _context.Message == null || _context.Chat == null)
            {
                return null;
            }
            Message? chosenMessage = _context.Message.Find(id2);
            if (chosenMessage == null)
            {
                return null;
            }
            var chosenChats = _context.Chat.Where(d => d.Id == chosenMessage.ChatId && (d.Name1 == id && d.Name2 == myId) || (d.Name1 == myId && d.Name2 == id));
            if (chosenChats == null || !chosenChats.Any())
            {
                return null;
            }
            var chosenChat = chosenChats.FirstOrDefault();
            if (chosenChat == null)
            {
                return null;
            }
            return chosenMessage;
        }

        [HttpGet("{id}/[action]/{id2}")]
        public IActionResult messages(string? id, int id2)
        {
            string myId = "string";
            Message? message = GetMessage(id, id2, myId);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(new { id = id2, content = message.Content, created = message.Created.ToString(), sent = (message.Author == myId) });
        }

        [HttpPut("{id}/[action]/{id2}")]
        public async Task<IActionResult> messages(string? id, int id2, [Bind("content")] Message message)
        {
            string myId = "string";
            Message? messageToEdit = GetMessage(id, id2, myId);
            if (messageToEdit == null)
            {
                return NotFound();
            }
            messageToEdit.Content = message.Content;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}/messages/{id2}")]
        public async Task<IActionResult> messagesDelete(string? id, int id2)
        {
            string myId = "string";
            Message? messageToDelete = GetMessage(id, id2, myId);
            if (messageToDelete == null)
            {
                return NotFound();
            }
            _context.Remove(messageToDelete);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> invitations([Bind("from,to,server")] Invitation invitation)
        {
            if (_context.UserContact == null || _context.Chat == null || _context.User == null)
            {
                return BadRequest();
            }
            if (_context.User.Find(invitation.To) == null)
            {
                return NotFound();
            }
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
            return Ok();
        }
    }
}
