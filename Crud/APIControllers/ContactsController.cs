using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud.Data;
using CrudApp.Models;
using Crud.Helpers;

namespace Crud.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly CrudContext _context;

        public ContactsController(CrudContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Contact>>> GetContact(int id)
        {
            if (_context.Contact == null)
            {
                return NotFound();
            }


            var contacts = await _context.Contact.Where(contact=>contact.information_ID == id).ToListAsync();
        
           
            if (contacts == null)
            {
                return NotFound();
            }

            return contacts;
        }


    }
}
