using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourTravel.Models;

namespace TourTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationAPIController : ControllerBase
    {

        #region Configuration Fields 
        private readonly TourManagementContext _context;
        public DestinationAPIController(TourManagementContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAllDestinations   
        [HttpGet]
        public async Task<IActionResult> GetDestination()
        {
            var destinations = await _context.MstDestinations.Include(u=>u.User)
                .Select(c => new
                {
                    c.DestinationId,
                    c.DestinationCode,
                    c.DestinationName,
                    c.Country,
                    c.Description,
                    c.UserId,
                    c.Created,
                    c.Modified,
                    UserName = c.User.UserName
                })
                .ToListAsync();
            return Ok(destinations);
        }
        #endregion

        #region GetDestinationById        
        [HttpGet("{DestinationID}")]
        public async Task<IActionResult> GetDestinationById(int DestinationID)
        {
            var destination = await _context.MstDestinations.FindAsync(DestinationID);
            if (destination == null)
            {
                return NotFound();
            }
            return Ok(destination);
        }

        #endregion

        #region DeleteDestinationById        
        [HttpDelete("{DestinationID}")]
        public async Task<IActionResult> DeleteDestinationById(int DestinationID)
        {
            var destination = await _context.MstDestinations.FindAsync(DestinationID);

            if (destination == null)
            {
                return NotFound();
            }

            _context.MstDestinations.Remove(destination);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertDestination
        [HttpPost]
        public async Task<IActionResult> InsertDestination(MstDestination destination)
        {
            await _context.MstDestinations.AddAsync(destination);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region UpdateDestination         
        [HttpPut("{DestinationID}")]
        public async Task<IActionResult> UpdateDestination(int DestinationID, MstDestination destination)
        {
            if (DestinationID != destination.DestinationId)
            {
                return BadRequest();
            }

            var existingDestination = await _context.MstDestinations.FindAsync(DestinationID);
            if (existingDestination == null)
            {
                return NotFound();
            }

            existingDestination.DestinationName = destination.DestinationName;
            existingDestination.DestinationCode = destination.DestinationCode;
            existingDestination.Description = destination.Description;
            existingDestination.Country = destination.Country;
            existingDestination.UserId = destination.UserId;
            existingDestination.Created = destination.Created;
            existingDestination.Modified = destination.Modified;

            _context.MstDestinations.Update(existingDestination);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region FilterOnDestination
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MstDestination>>> Filter([FromQuery] string? DestinationName, [FromQuery] string? DestinationCode, [FromQuery] string? Country)
        {
            var query = _context.MstDestinations.Include(u => u.User).AsQueryable();
            //Include means User table ma jetla record 6 aene mare Package sathe map krva 6

            if (!string.IsNullOrWhiteSpace(DestinationName))
            {
                query = query.Where(p => p.DestinationName.Contains(DestinationName));
            }
            if (!string.IsNullOrWhiteSpace(DestinationCode))
            {
                query = query.Where(p => p.DestinationCode.Contains(DestinationCode));
            }
            if (!string.IsNullOrWhiteSpace(Country))
            {
                query = query.Where(p => p.Country.Contains(Country));
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

    }
}