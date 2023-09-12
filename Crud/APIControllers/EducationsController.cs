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

namespace Crud.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly CrudContext _context;
        private readonly IMapper _mapper;

        public EducationsController(CrudContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<EducationsReadDTOs>>> GetEducations(int id)
        {

            if (_context.Education == null)
            {
                return NotFound();
            }
            var educations = await _context.Education.Where(education => education.information_ID == id).ToListAsync();

            if (educations == null)
            {
                return NotFound();
            }
            var educationsDTO = _mapper.Map<List<EducationsReadDTOs>>(educations);

            return educationsDTO;
        }

        

        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducation(int id, EducationsUpdateDTOs educationUpdateDTOs)
        {
            if(_context.Education == null)
            {
                return BadRequest();
            }

            var edu = await _context.Education.FindAsync(id);
            if (id != educationUpdateDTOs.education_id)
            {
                return BadRequest();
            }
            if (edu == null)
            {
                throw new Exception($"Education {id} is not found.");
            }
            _mapper.Map(educationUpdateDTOs, edu);
            _context.Education.Update(edu);
            await _context.SaveChangesAsync();
            var eduReadDTO = _mapper.Map<EducationsReadDTOs>(edu);
            return Ok(eduReadDTO);
        }

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EducationsReadDTOs>> PostEducation(EducationsCreateDTOs educationCreateDTOs)
        {
          if (_context.Education == null)
          {
              return Problem("Entity set 'CrudContext.Education'  is null.");
          }
            var education = _mapper.Map<Education>(educationCreateDTOs);
            _context.Education.Add(education);
            await _context.SaveChangesAsync();
            var educationReadDTO = _mapper.Map<EducationsReadDTOs>(education);
            return CreatedAtAction("GetEducation", new { id = educationReadDTO.education_id }, educationReadDTO);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            if (_context.Education == null)
            {
                return NotFound();
            }
            var education = await _context.Education.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }

            _context.Education.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationExists(int id)
        {
            return (_context.Education?.Any(e => e.education_id == id)).GetValueOrDefault();
        }
    }
}
