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
using AutoMapper;
using Crud.DTOs.InformationDTOs;

namespace Crud.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly CrudContext _context;
        private readonly IMapper _mapper;


        public InformationController(CrudContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Information
        [HttpGet]
        public async Task<ActionResult<List<InformationReadDTOs>>> GetInformation()
        {
            if (_context.Information == null)
            {
                return NotFound();
            }
            var informations = await _context.Information.ToListAsync();
            var records = _mapper.Map<List<InformationReadDTOs>>(informations);
            return records;
        }

        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformationReadDTOs>> GetInformation(int id)
        {
            if (_context.Information == null)
            {
                return NotFound();
            }
            var information = await _context.Information.FindAsync(id);

            if (information == null)
            {
                return NotFound();
            }
            var informationsDTO = _mapper.Map<InformationReadDTOs>(information);
            return informationsDTO;
        }

        // PUT: api/Information/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformation(int id, InformationUpdateDTOs informationUpdateDTOs)
        {
            var info = await _context.Information.FindAsync(id);
            if (id != informationUpdateDTOs.information_id)
            {
                return BadRequest();
            }
            if (info == null)
            {
                throw new Exception($"Information {id} is not found.");
            }
            _mapper.Map(informationUpdateDTOs, info);
            _context.Information.Update(info);
            await _context.SaveChangesAsync();
            var infoReadDTO = _mapper.Map<InformationReadDTOs>(info);
            return Ok(infoReadDTO);
        }



        // POST: api/Information
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InformationReadDTOs>> PostInformation(InformationCreateDTOs informationCreateDTOs)
        {
            if (_context.Information == null)
            {
                return Problem("Entity set 'CrudContext.Information'  is null.");
            }
            var information = _mapper.Map<Information>(informationCreateDTOs);
            _context.Information.Add(information);
            await _context.SaveChangesAsync();
            var informationsReadDTO = _mapper.Map<InformationReadDTOs>(information);
            return CreatedAtAction("GetInformation", new { id = information.information_id }, informationsReadDTO);
        }

        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            if (_context.Information == null)
            {
                return NotFound();
            }
            var information = await _context.Information.FindAsync(id);
            if (information == null)
            {
                return NotFound();
            }

            _context.Information.Remove(information);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InformationExists(int id)
        {
            return (_context.Information?.Any(e => e.information_id == id)).GetValueOrDefault();
        }
    }
}
