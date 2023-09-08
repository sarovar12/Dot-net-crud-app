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
using Microsoft.AspNetCore.Mvc.Filters;

namespace Crud.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private const string UserName = "username";
        private const string Password = "password";
        private readonly CrudContext _context;

        public InformationController(CrudContext context)
        {
            _context = context;
        }



        [HttpGet]
        [Route("/api/login")]

        public async Task<ActionResult<Information>> GetLogin()
        {
            if (!HttpContext.Request.Headers.TryGetValue(UserName, out var username) ||
                !HttpContext.Request.Headers.TryGetValue(Password, out var password))
            {
                return BadRequest("bad boy");
            }
            else
            {

                var userValid = await _context.Information.Where(info => info.name == username.ToString()).FirstOrDefaultAsync();
                if(userValid == null)
                {
                    return NotFound("Username not found");
                }
                if(userValid.password != password.ToString())
                {
                    return NotFound("Wrong password");
                }
                userValid.password = null;

                return userValid;
                

                
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Information>> GetInformation(int id)
        {
            var information = await _context.Information.Where(info => info.information_id == id).FirstOrDefaultAsync();

            if (information == null)
            {
                return NotFound();
            }        

            return Ok(information);
        }
    }
}
