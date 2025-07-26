using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourTravel.Models;

namespace TourTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentAPIController : ControllerBase
    {
        #region Configuration Fields 
        private readonly TourManagementContext _context;
        public PaymentAPIController(TourManagementContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAllPayment       
        [HttpGet]
        public async Task<IActionResult> GetPayment()
        {
            var payments = await _context.Payments.ToListAsync();
            return Ok(payments);
        }
        #endregion

        #region GetPaymentById        
        [HttpGet("{PaymentID}")]
        public async Task<IActionResult> GetPaymentById(int PaymentID)
        {
            var payment = await _context.Payments.FindAsync(PaymentID);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }
        #endregion

        #region DeletePaymentById        
        [HttpDelete("{PaymentID}")]
        public async Task<IActionResult> DeletePaymentById(int PaymentID)
        {
            var payment = await _context.Payments.FindAsync(PaymentID);

            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertPayment 
        [HttpPost]
        public async Task<IActionResult> InsertPayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region UpdatePayment         
        [HttpPut("{UserID}")]
        public async Task<IActionResult> UpdatePayment(int PaymentID, Payment payment)
        {
            if (PaymentID != payment.PaymentId)
            {
                return BadRequest();
            }

            var existingPayment = await _context.Payments.FindAsync(PaymentID);
            if (existingPayment == null)
            {
                return NotFound();
            }

            existingPayment.BookingId = payment.BookingId;
            existingPayment.PaymentDate = payment.PaymentDate;
            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentMethod = payment.PaymentMethod;
            existingPayment.TransactionId = payment.TransactionId;
            existingPayment.UserId = payment.UserId;
            existingPayment.Created = payment.Created;
            existingPayment.Modified = payment.Modified;

            _context.Payments.Update(existingPayment);
            await _context.SaveChangesAsync();

            return NoContent();
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
        // Get all booking (for dropdown)
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