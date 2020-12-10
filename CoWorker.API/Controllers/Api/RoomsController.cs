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

namespace CoWorker.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoomsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomViewModel>>> GetRooms()
        {
            return _mapper.Map<List<RoomViewModel>>(await _context.Rooms.ToListAsync());
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomViewModel>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return _mapper.Map<RoomViewModel>(room);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutRoom(int id, RoomViewModel roomViewModel)
        {
            Room room = _mapper.Map<Room>(roomViewModel);

            if (id != room.Id)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<RoomViewModel>> PostRoom(RoomViewModel roomViewModel)
        {
            Room room = _mapper.Map<Room>(roomViewModel);

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            roomViewModel.Id = room.Id;
            
            return CreatedAtAction("GetRoom", new { id = room.Id }, roomViewModel);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<RoomViewModel>> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoomViewModel>(room);
        }

        [HttpGet("ChangeRoomSettings/{roomId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<RoomViewModel>> ChangeRoomSettings(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);

            if (room == null)
            {
                return NotFound();
            }

            var bookingRecords = await _context.BookingRecords.Where(b => b.RoomId == roomId).ToListAsync();
            if (bookingRecords.Count == 0) return NoContent();
            DateTime currentDate = DateTime.UtcNow;

            foreach(var record in bookingRecords)
            {
                if (record.StartTime <= currentDate && record.EndTime >= currentDate) room.CurrentSettingsId = record.ApplicationUser.Settings.LastOrDefault().Id;
            }

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<RoomViewModel>(room));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }

    }
}
