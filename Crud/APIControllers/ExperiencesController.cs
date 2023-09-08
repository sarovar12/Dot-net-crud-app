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
    public class ExperiencesController : ControllerBase
    {
        private readonly CrudContext _context;

        public ExperiencesController(CrudContext context)
        {
            _context = context;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<List<Experience>>> GetExperience(int id)
        {
          if (_context.Experience == null)
          {
              return NotFound();
          }
            var experience = await _context.Experience.Where(experience => experience.information_id == id).ToListAsync();

            if (experience == null)
            {
                return NotFound();
            }

            return experience;
        }
              
    }
}
