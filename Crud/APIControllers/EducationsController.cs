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
    public class EducationsController : ControllerBase
    {
        private readonly CrudContext _context;

        public EducationsController(CrudContext context)
        {
            _context = context;
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<List<Education>>> GetEducation(int id)
        {
          if (_context.Education == null)
          {
              return NotFound();
          }
            var education = await _context.Education.Where(edu=>edu.information_ID == id).ToListAsync();

            if (education == null)
            {
                return NotFound();
            }

            return education;
        }



    }
}
