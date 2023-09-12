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
using Crud.DTOs.InformationDTOs;
using Microsoft.VisualBasic;
using Crud.DTOs.EducationsDTOs;

namespace Crud.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly CrudContext _context;
        private readonly IMapper _mapper;

        public SkillsController(CrudContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SkillReadDTOs>>> GetSkills(int id)
        {

            if (_context.Skills == null)
            {
                return NotFound();
            }
            var skills = await _context.Skills.Where(skill => skill.information_id == id).ToListAsync();

            if (skills == null)
            {
                return NotFound();
            }
            var skillDTO = _mapper.Map<List<SkillReadDTOs>>(skills);

            return skillDTO;
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<SkillReadDTOs>> PutSkills(int id, SkillsUpdateDTOs skillsUpdateDTOs)
        {
            if(_context.Skills == null)
            {
                return BadRequest();
            }

            var skill = await _context.Skills.FindAsync(id);
            if (id != skillsUpdateDTOs.skill_id)
            {
                return BadRequest();
            }
            if (skill == null)
            {
                throw new Exception($"Education {id} is not found.");
            }
            _mapper.Map(skillsUpdateDTOs, skill);
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
            var skillReadDTO = _mapper.Map<SkillReadDTOs>(skill);
            return Ok(skillReadDTO);

        }

        
        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SkillReadDTOs>> PostSkills(SkillCreateDTOs skillCreateDTO)
        {

          if (_context.Skills == null)
          {
              return Problem("Entity set 'CrudContext.Skills'  is null.");
          }
            var skill = _mapper.Map<Skills>(skillCreateDTO);
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            var skillsReadDTO = _mapper.Map<SkillReadDTOs>(skill);
            return CreatedAtAction("GetSkills", new { id = skillsReadDTO.skill_id }, skillsReadDTO);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkills(int id)
        {
            if (_context.Skills == null)
            {
                return NotFound();
            }
            var skills = await _context.Skills.FindAsync(id);
            if (skills == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skills);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkillsExists(int id)
        {
            return (_context.Skills?.Any(e => e.skill_id == id)).GetValueOrDefault();
        }
    }
}
