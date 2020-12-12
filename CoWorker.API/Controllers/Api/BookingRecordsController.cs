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
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace CoWorker.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    public class BookingRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public BookingRecordsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: api/BookingRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingRecordViewModel>>> GetBookingRecords()
        {
            return _mapper.Map<List<BookingRecordViewModel>>(await _context.BookingRecords.ToListAsync());
        }

        // GET: api/BookingRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingRecordViewModel>> GetBookingRecord(int id)
        {
            var bookingRecord = await _context.BookingRecords.FindAsync(id);

            if (bookingRecord == null)
            {
                return NotFound();
            }

            return _mapper.Map<BookingRecordViewModel>(bookingRecord);
        }
        [HttpGet("byroom/{id}")]
        public async Task<ActionResult> GetBookingRecordsByRoom(int id)
        {
            var bookingRecords = (await _context.BookingRecords.Where(b => b.RoomId == id).ToListAsync()).Where(b => IsToday(b.StartTime));

            if (bookingRecords == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<BookingRecordViewModel>>(bookingRecords));
        }
        private bool IsToday(DateTime? startTime)
        {
            return startTime.GetValueOrDefault().DayOfYear == DateTime.Now.DayOfYear;
        }

        // PUT: api/BookingRecords/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingRecord(int id, BookingRecordViewModel bookingRecordViewModel)
        {
            BookingRecord bookingRecord = _mapper.Map<BookingRecord>(bookingRecordViewModel);

            if (id != bookingRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookingRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingRecordExists(id))
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

        // POST: api/BookingRecords
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BookingRecordViewModel>> PostBookingRecord(BookingRecordViewModel bookingRecordViewModel)
        {
            BookingRecord bookingRecord = _mapper.Map<BookingRecord>(bookingRecordViewModel);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(bookingRecordViewModel?.ApplicationUser?.Id == null) bookingRecord.ApplicationUserId = user.Id;

            
            _context.BookingRecords.Add(bookingRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookingRecord", new { id = bookingRecord.Id }, bookingRecordViewModel);
        }

        // DELETE: api/BookingRecords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookingRecordViewModel>> DeleteBookingRecord(int id)
        {
            var bookingRecord = await _context.BookingRecords.FindAsync(id);
            if (bookingRecord == null)
            {
                return NotFound();
            }

            _context.BookingRecords.Remove(bookingRecord);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingRecordViewModel>(bookingRecord);
        }

        private bool BookingRecordExists(int id)
        {
            return _context.BookingRecords.Any(e => e.Id == id);
        }
    }
}
