using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourTravel.Models;
namespace TourTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItineraryAPIController : ControllerBase
    {

        #region Configuration Fields 
        private readonly TourManagementContext _context;
        public ItineraryAPIController(TourManagementContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAllItinerary       
        [HttpGet]
        public async Task<IActionResult> GetItinerary()
        {
            var itineraries = await _context.Itineraries.Include(u => u.User).Include(u => u.Package)
                .Select(c => new
                {
                    c.ItineraryId,
                    c.PackageId,
                    c.DayNumber,
                    c.ActivityName,
                    c.ActivityDescription,
                    c.LocationDetails,
                    c.StartTime,
                    c.EndTime,
                    c.UserId,
                    c.Created,
                    c.Modified,
                    UserName = c.User.UserName,
                    PackageName = c.Package.PackageName
                })
                .ToListAsync();
            return Ok(itineraries);
        }
        #endregion

        #region GetItineraryById        
        [HttpGet("{ItineraryID}")]
        public async Task<IActionResult> GetItineraryById(int ItineraryID)
        {
            var itinerary = await _context.Itineraries.FindAsync(ItineraryID);
            if (itinerary == null)
            {
                return NotFound();
            }
            return Ok(itinerary);
        }
        #endregion

        #region DeleteItineraryById        
        [HttpDelete("{ItineraryID}")]
        public async Task<IActionResult> DeleteItineraryById(int ItineraryID)
        {
            var itinerary = await _context.Itineraries.FindAsync(ItineraryID);

            if (itinerary == null)
            {
                return NotFound();
            }

            _context.Itineraries.Remove(itinerary);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertItinerary 
        [HttpPost]
        public async Task<IActionResult> InsertItinerary(Itinerary itinerary)
        {
            await _context.Itineraries.AddAsync(itinerary);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region UpdateItinerary         
        [HttpPut("{ItineraryID}")]
        public async Task<IActionResult> UpdateItinerary(int ItineraryID, Itinerary itinerary)
        {
            if (ItineraryID != itinerary.ItineraryId)
            {
                return BadRequest();
            }

            var existingItinerary = await _context.Itineraries.FindAsync(ItineraryID);
            if (existingItinerary == null)
            {
                return NotFound();
            }

            existingItinerary.PackageId = itinerary.PackageId;
            existingItinerary.DayNumber = itinerary.DayNumber;
            existingItinerary.ActivityName = itinerary.ActivityName;
            existingItinerary.ActivityDescription = itinerary.ActivityDescription;
            existingItinerary.LocationDetails = itinerary.LocationDetails;
            existingItinerary.StartTime = itinerary.StartTime;
            existingItinerary.EndTime = itinerary.EndTime;
            existingItinerary.UserId = itinerary.UserId;
            existingItinerary.Created = itinerary.Created;
            existingItinerary.Modified = itinerary.Modified;

            _context.Itineraries.Update(existingItinerary);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region FilterOnItinerary
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Itinerary>>> Filter([FromQuery] string? DayNumber, [FromQuery] string? LocationDetails)
        {
            var query = _context.Itineraries.AsQueryable(); // badha int para lakhvana
            //Include means User table ma jetla record 6 aene mare Package sathe map krva 6

            if (!string.IsNullOrWhiteSpace(DayNumber))
            {
                query = query.Where(p => p.DayNumber.EndsWith(DayNumber));
            }
            if (!string.IsNullOrWhiteSpace(LocationDetails))
            {
                query = query.Where(p => p.LocationDetails.Contains(LocationDetails));
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

        #region PackageDropDown
        // Get all Users (for dropdown)
        [HttpGet("dropdown/Package")]
        public async Task<ActionResult<IEnumerable<object>>> GetPackages()
        {
            return await _context.MstPackages
                .Select(c => new { c.PackageId, c.PackageName })
                .ToListAsync();
        }
        #endregion
    }
}