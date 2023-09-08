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
    public class SkillsController : ControllerBase
    {
        private readonly CrudContext _context;

        public SkillsController(CrudContext context)
        {
            _context = context;
        }



        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Skills>>> GetSkills(int id)
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

            return skills;
        }




    }
}
