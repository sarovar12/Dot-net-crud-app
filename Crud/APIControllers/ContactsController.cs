using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud.Data;
using CrudApp.Models;
using Crud.DTOs.SkillsDTOs;
using AutoMapper;
using Crud.DTOs.EducationsDTOs;
using Crud.DTOs.ExperiencesDTOs;
using Crud.DTOs.ContactsDTOs;

namespace Crud.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly CrudContext _context;
        private readonly IMapper _mapper;

        public ContactsController(CrudContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ContactReadDTOs>>> GetContacts(int id)
        {

            if (_context.Contact == null)
            {
                return NotFound();
            }
            var contacts = await _context.Contact.Where(contact => contact.information_ID == id).ToListAsync();

            if (contacts == null)
            {
                return NotFound();
            }
            var contactsDTO = _mapper.Map<List<ContactReadDTOs>>(contacts);

            return contactsDTO;
        }



        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacts(int id, ContactUpdateDTOs contactUpdateDTOs)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (id != contactUpdateDTOs.contact_id)
            {
                return BadRequest();
            }
            if (contact == null)
            {
                throw new Exception($"Education {id} is not found.");
            }
            _mapper.Map(contactUpdateDTOs, contact);
            _context.Contact.Update(contact);
            await _context.SaveChangesAsync();
            var contactReadDTO = _mapper.Map<ContactReadDTOs>(contact);
            return Ok(contactReadDTO);
        }

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactReadDTOs>> PostContact(ContactCreateDTOs contactCreateDTOs)
        {
            if (_context.Contact == null)
            {
                return Problem("Entity set 'CrudContext.Education'  is null.");
            }
            var contact = _mapper.Map<Contact>(contactCreateDTOs);
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();
            var contactReadDTO = _mapper.Map<ContactReadDTOs>(contact);
            return CreatedAtAction("GetEducation", new { id = contactReadDTO.contact_id }, contactReadDTO);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (_context.Contact == null)
            {
                return NotFound();
            }
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return (_context.Contact?.Any(e => e.contact_id == id)).GetValueOrDefault();
        }
    }
}
