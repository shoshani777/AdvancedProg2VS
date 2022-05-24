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
        public async Task<ICollection<UserContact>> IndexAsync()
        {
            return _context.UserContact != null ?
                         await _context.UserContact.ToListAsync() : new List<UserContact> { };
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
        public IActionResult Edit(string? id, [Bind("NickName,Server")] UserContact contact)
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
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string? id)
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
            _context.UserContact.Remove(chosenContact);
            _context.SaveChanges();
            return Ok();
        }

    }
}
