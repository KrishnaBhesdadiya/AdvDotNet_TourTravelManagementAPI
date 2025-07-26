using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourTravel.Models;

namespace TourTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingAPIController : ControllerBase
    {
        #region Configuration Fields 
        private readonly TourManagementContext _context;
        public BookingAPIController(TourManagementContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAllBooking       
        [HttpGet]
        public async Task<IActionResult> GetBooking()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return Ok(bookings);
        }
        #endregion

        #region GetBookingById        
        [HttpGet("{BookingID}")]
        public async Task<IActionResult> GetBookingById(int BookingID)
        {
            var booking = await _context.Bookings.FindAsync(BookingID);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        #endregion

        #region DeleteBookingById        
        [HttpDelete("{BookingID}")]
        public async Task<IActionResult> DeleteBookingById(int BookingID)
        {
            var booking = await _context.Bookings.FindAsync(BookingID);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertBooking 
        [HttpPost]
        public async Task<IActionResult> InsertBooking(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region UpdateBooking         
        [HttpPut("{BookingID}")]
        public async Task<IActionResult> UpdateBooking(int BookingID, Booking booking)
        {
            if (BookingID != booking.BookingId)
            {
                return BadRequest();
            }

            var existingBooking = await _context.Bookings.FindAsync(BookingID);
            if (existingBooking == null)
            {
                return NotFound();
            }

            existingBooking.CustomerId = booking.CustomerId;
            existingBooking.PackageId = booking.PackageId;
            existingBooking.BookingDate = booking.BookingDate;
            existingBooking.TravelStartDate = booking.TravelStartDate;
            existingBooking.NumberOfAdults = booking.NumberOfAdults;
            existingBooking.NumberOfChildren = booking.NumberOfChildren;
            existingBooking.TotalBookingPrice = booking.TotalBookingPrice;
            existingBooking.PaymentStatus = booking.PaymentStatus;
            existingBooking.BookingStatus = booking.BookingStatus;
            existingBooking.SpecialRequests = booking.SpecialRequests;
            existingBooking.UserId = booking.UserId;
            existingBooking.Created = booking.Created;
            existingBooking.Modified = booking.Modified;

            _context.Bookings.Update(existingBooking);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region FilterOnBooking
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Booking>>> Filter([FromQuery] string? BookingStatus, [FromQuery] string? PaymentStatus)
        {
            var query = _context.Bookings.AsQueryable(); // badha int para lakhvana
            //Include means User table ma jetla record 6 aene mare Package sathe map krva 6

            if (!string.IsNullOrWhiteSpace(BookingStatus))
            {
                query = query.Where(p => p.BookingStatus.EndsWith(BookingStatus));
            }
            if (!string.IsNullOrWhiteSpace(PaymentStatus))
            {
                query = query.Where(p => p.PaymentStatus.Contains(PaymentStatus));
            }
            return await query.ToListAsync();
        }
        #endregion

        #region CustomerDropDown
        // Get all Customers (for dropdown)
        [HttpGet("dropdown/Customer")]
        public async Task<ActionResult<IEnumerable<object>>> GetCustomers()
        {
            return await _context.MstCustomers
                .Select(c => new { c.CustomerId, c.FirstName })
                .ToListAsync();
        }
        #endregion

        #region PackageDropDown
        // Get all Packages (for dropdown)
        [HttpGet("dropdown/Package")]
        public async Task<ActionResult<IEnumerable<object>>> GetPackages()
        {
            return await _context.MstPackages
                .Select(c => new { c.PackageId, c.PackageName })
                .ToListAsync();
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
    }
}