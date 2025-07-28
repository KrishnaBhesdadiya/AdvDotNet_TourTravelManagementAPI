using FluentValidation;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourTravel.Models;

namespace TourTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {

        #region Configuration Fields 
        private readonly TourManagementContext _context;
        private readonly IValidator<MstUser> _validator;
        public UserAPIController(TourManagementContext context, IValidator<MstUser> validator)
        {
            _context = context;
            _validator = validator;
        }
        #endregion

        #region GetAllUser       
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _context.MstUsers.ToListAsync();
            return Ok(users);
        }
        #endregion

        #region GetUserById        
        [HttpGet("{UserID}")]
        public async Task<IActionResult> GetUserById(int UserID)
        {
            var user = await _context.MstUsers.FindAsync(UserID);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        #endregion

        #region DeleteUserById        
        [HttpDelete("{UserID}")]
        public async Task<IActionResult> DeleteUserById(int UserID)
        {
            var user = await _context.MstUsers.FindAsync(UserID);

            if (user == null)
            {
                return NotFound();
            }

            _context.MstUsers.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertUser 
        [HttpPost]
        public async Task<IActionResult> InsertUser(MstUser user)
        {
            var validationResult = await _validator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }
            await _context.MstUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region UpdateUser         
        [HttpPut("{UserID}")]
        public async Task<IActionResult> UpdateUser(int UserID, MstUser user)
        {
            if (UserID != user.UserId)
            {
                return BadRequest();
            }

            var existingUser = await _context.MstUsers.FindAsync(UserID);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;
            existingUser.MobileNo = user.MobileNo;
            existingUser.Email = user.Email;
            existingUser.Modified = user.Modified;

            _context.MstUsers.Update(existingUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region FilterOnUser
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MstUser>>> Filter([FromQuery] string? UserName, [FromQuery] string? Role, [FromQuery] string? Email)
        {
            var query = _context.MstUsers.AsQueryable(); // badha int para lakhvana
            //Include means User table ma jetla record 6 aene mare Package sathe map krva 6

            if (!string.IsNullOrWhiteSpace(UserName))
            {
                query = query.Where(p => p.UserName.Contains(UserName));
            }
            if (!string.IsNullOrWhiteSpace(Role))
            {
                query = query.Where(p => p.Role.Contains(Role));
            }
            if (!string.IsNullOrWhiteSpace(Email))
            {
                query = query.Where(p => p.Email.Contains(Email));
            }
            return await query.ToListAsync();
        }
        #endregion

        #region TotalUser       
        [HttpGet("UserCount")]
        public async Task<IActionResult> UserCount()
        {
            var userCount = await _context.MstUsers.CountAsync();
            return Ok(new {Count = userCount});
        }
        #endregion

        #region AdminStats
        [HttpGet("admin/stats")]
        public IActionResult GetAdminStats()
        {
            var stats = new
            {
                UserCount = _context.MstUsers.Count(),
                CustomerCount = _context.MstCustomers.Count(),
                PackageCount = _context.MstPackages.Count(),
                DestinationCount = _context.MstDestinations.Count(),
                PackageDestinationCount = _context.PackageDestinations.Count(),
                ItineraryCount = _context.Itineraries.Count(),
                BookingCount = _context.Bookings.Count(),
                PaymentCount = _context.Payments.Count(),
                TravelerCount = _context.MstTravelers.Count()
            };

            return Ok(stats);
        }
        #endregion
    }
}
