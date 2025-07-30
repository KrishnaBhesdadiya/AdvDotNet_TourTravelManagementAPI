using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourTravel.Models;

namespace TourTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelerAPIController : ControllerBase
    {
        #region Configuration Fields 
        private readonly TourManagementContext _context;
        public TravelerAPIController(TourManagementContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAllTraveler       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MstTraveler>>> GetTraveler()
        {
            var traveler = await _context.MstTravelers.Include(u => u.User).Include(u => u.Booking)
                .Select(c => new
                {
                    c.TravelerId,
                    c.BookingId,
                    c.FirstName,
                    c.LastName,
                    c.DateOfBirth,
                    c.PassportNumber,
                    c.PassportExpiryDate,
                    c.EmergencyContactName,
                    c.EmergencyContactNumber,
                    c.UserId,
                    c.Created,
                    c.Modified,
                    UserName = c.User.UserName,
                    BookingCode = c.Booking.BookingCode
                })
                .ToListAsync();
            return Ok(traveler);
        }
        #endregion

        #region GetTravelerById        
        [HttpGet("{TravelerID}")]
        public async Task<IActionResult> GetTravelerById(int TravelerID)
        {
            var traveler = await _context.MstTravelers.FindAsync(TravelerID);

            if (traveler == null)
            {
                return NotFound();
            }

            return Ok(traveler);
        }

        #endregion

        #region DeleteTravelerById        
        [HttpDelete("{TravelerID}")]
        public async Task<IActionResult> DeleteTravelerById(int TravelerID)
        {
            var traveler = await _context.MstTravelers.FindAsync(TravelerID);

            if (traveler == null)
            {
                return NotFound();
            }

            _context.MstTravelers.Remove(traveler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region InsertTraveler
        [HttpPost]
        public async Task<IActionResult> InsertTraveler(MstTraveler traveler)
        {
            await _context.MstTravelers.AddAsync(traveler);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region UpdateTraveler        
        [HttpPut("{TravelerID}")]
        public async Task<IActionResult> UpdateTraveler(int TravelerID, MstTraveler traveler)
        {
            if (TravelerID != traveler.TravelerId)
            {
                return BadRequest();
            }

            var existingTraveler = await _context.MstTravelers.FindAsync(TravelerID);
            if (existingTraveler == null)
            {
                return NotFound();
            }

            existingTraveler.BookingId = traveler.BookingId;
            existingTraveler.FirstName = traveler.FirstName;
            existingTraveler.LastName = traveler.LastName;
            existingTraveler.DateOfBirth = traveler.DateOfBirth;
            existingTraveler.PassportNumber = traveler.PassportNumber;
            existingTraveler.PassportExpiryDate = traveler.PassportExpiryDate;
            existingTraveler.EmergencyContactName = traveler.EmergencyContactName;
            existingTraveler.EmergencyContactNumber = traveler.EmergencyContactNumber;
            existingTraveler.UserId = traveler.UserId;
            existingTraveler.Created = traveler.Created;
            existingTraveler.Modified = traveler.Modified;

            _context.MstTravelers.Update(existingTraveler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region FilterOnTraveler
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MstTraveler>>> Filter([FromQuery] string? FirstName, [FromQuery] string? LastName)
        {
            var query = _context.MstTravelers.AsQueryable(); // badha int para lakhvana
            //Include means User table ma jetla record 6 aene mare Package sathe map krva 6

            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                query = query.Where(p => p.FirstName.Contains(FirstName));
            }
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                query = query.Where(p => p.LastName.Contains(LastName));
            }
            return await query.ToListAsync();
        }
        #endregion

        #region UserDropDown
        // Get all Users (for dropdown)
        [HttpGet("dropdown/Users")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            return await _context.MstUsers
                .Select(c => new { c.UserId, c.UserName })
                .ToListAsync();
        }
        #endregion

        #region BookingDropDown
        // Get all Booking (for dropdown)
        [HttpGet("dropdown/Booking")]
        public async Task<ActionResult<IEnumerable<object>>> GetBookings()
        {
            return await _context.Bookings
                .Select(c => new { c.BookingId, c.BookingCode })
                .ToListAsync();
        }
        #endregion
    }
}