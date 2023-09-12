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

using Crud.DTOs.ExperiencesDTOs;
using Crud.DTOs.EducationsDTOs;

namespace Crud.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly CrudContext _context;
        private readonly IMapper _mapper;

        public ExperiencesController(CrudContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ExperiencesReadDTOs>>> GetExperiences(int id)
        {

            if (_context.Experience == null)
            {
                return NotFound();
            }
            var experiences = await _context.Experience.Where(experiences => experiences.information_id == id).ToListAsync();

            if (experiences == null)
            {
                return NotFound();
            }
            var experiencesDTO = _mapper.Map<List<ExperiencesReadDTOs>>(experiences);

            return experiencesDTO ;
        }



        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperiences(int id, ExperiencesUpdateDTOs experienceUpdateDTOs)
        {
            var experience = await _context.Experience.FindAsync(id);
            if (id != experienceUpdateDTOs.experience_id)
            {
                return BadRequest();
            }
            if (experience == null)
            {
                throw new Exception($"Education {id} is not found.");
            }
            _mapper.Map(experienceUpdateDTOs, experience);
            _context.Experience.Update(experience);
            await _context.SaveChangesAsync();
            var eduReadDTO = _mapper.Map<ExperiencesReadDTOs>(experience);
            return Ok(eduReadDTO);
        }

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExperiencesReadDTOs>> PostExperiences(ExperiencesCreateDTOs experiencesCreateDTOs)
        {
            if (_context.Experience == null)
            {
                return Problem("Entity set 'CrudContext.Education'  is null.");
            }
            var experience = _mapper.Map<Experience>(experiencesCreateDTOs);
            _context.Experience.Add(experience);
            await _context.SaveChangesAsync();
            var experienceReadDTO = _mapper.Map<ExperiencesReadDTOs>(experience);
            return CreatedAtAction("GetEducation", new { id = experienceReadDTO.experience_id }, experienceReadDTO);
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
