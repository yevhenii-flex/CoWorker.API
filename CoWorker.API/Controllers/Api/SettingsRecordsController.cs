using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoWorker.API.Data;
using CoWorker.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CoWorker.API.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace CoWorker.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class SettingsRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public SettingsRecordsController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/SettingsRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SettingsRecordViewModel>>> GetSettingsRecords()
        {
            return _mapper.Map<List<SettingsRecordViewModel>>(await _context.SettingsRecords.ToListAsync());
        }

        // GET: api/SettingsRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SettingsRecordViewModel>> GetSettingsRecord(int id)
        {
            var settingsRecord = await _context.SettingsRecords.FindAsync(id);

            if (settingsRecord == null) 
            {
                return NotFound();
            }

            return _mapper.Map<SettingsRecordViewModel>(settingsRecord);
        }

        // PUT: api/SettingsRecords/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> PutSettingsRecord(int id, SettingsRecordViewModel settingsRecordViewModel)
        {
            SettingsRecord settingsRecord = _mapper.Map<SettingsRecord>(settingsRecordViewModel);
            
            if (id != settingsRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(settingsRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SettingsRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SettingsRecords
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<SettingsRecordViewModel>> PostSettingsRecord(SettingsRecordViewModel settingsRecordViewModel)
        {
            SettingsRecord settingsRecord = _mapper.Map<SettingsRecord>(settingsRecordViewModel);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (settingsRecordViewModel.ApplicationUser.Id == null) settingsRecordViewModel.ApplicationUser.Id = user.Id;

            _context.SettingsRecords.Add(settingsRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSettingsRecord", new { id = settingsRecord.Id }, settingsRecord);
        }

        // DELETE: api/SettingsRecords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SettingsRecordViewModel>> DeleteSettingsRecord(int id)
        {
            var settingsRecord = await _context.SettingsRecords.FindAsync(id);
            if (settingsRecord == null)
            {
                return NotFound();
            }

            _context.SettingsRecords.Remove(settingsRecord);
            await _context.SaveChangesAsync();

            return _mapper.Map<SettingsRecordViewModel>(settingsRecord);
        }

        private bool SettingsRecordExists(int id)
        {
            return _context.SettingsRecords.Any(e => e.Id == id);
        }
    }
}
