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
using Crud.Repositories;
namespace Crud.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly CrudContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;
        public InformationController(CrudContext context, IMapper mapper, IGenericRepos genericRepos)
        {
            _context = context;
            _mapper = mapper;
            _genericRepos = genericRepos;
        }
        // GET: api/Information
        [HttpGet]
        public async Task<ActionResult<List<InformationReadDTOs>>> GetInformation()
        {
            //if (_context.Information == null)
            //{
            //    return NotFound();
            //}
            var informations = await _genericRepos.GetAll<Information>();
            var records = _mapper.Map<List<InformationReadDTOs>>(informations);
            return records;
        }
        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformationReadDTOs>> GetInformation(int id)
        {
            var information = await _genericRepos.GetOne<Information>(id);
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
            var info = await _genericRepos.GetOne<Information>(id);
            if (id != informationUpdateDTOs.information_id)
            {
                return BadRequest();
            }
            if (info == null)
            {
                throw new Exception($"Information {id} is not found.");
            }
            _mapper.Map(informationUpdateDTOs, info);
            info = await _genericRepos.Update<Information>(id, info);
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
            await _genericRepos.Add(information);
            if (information != null)
            {
                var informationsReadDTO = _mapper.Map<InformationReadDTOs>(information);
                return CreatedAtAction("GetInformation", new { id = information.information_id }, informationsReadDTO);
            }
            return BadRequest();
        }
        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {

            var infoID = await _genericRepos.GetOne<Information>(id);

            if(infoID == null)
            {
                return NotFound();
            }

            try
            {
                await _genericRepos.Delete<Information>(id);
                return NoContent();
            }
            catch
            {
                return BadRequest();

            }
        }
        
    }
}