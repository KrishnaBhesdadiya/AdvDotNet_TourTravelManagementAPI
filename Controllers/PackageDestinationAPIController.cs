using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourTravel.Models;

namespace TourTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageDestinationAPIController : ControllerBase
    {

        #region Configuration Fields 
        private readonly TourManagementContext _context;
        public PackageDestinationAPIController(TourManagementContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAllPackageWiseDesignations   
        [HttpGet]
        public async Task<IActionResult> GetPackageWiseDesignations()
        {
            var designations = await _context.PackageDestinations.ToListAsync();
            return Ok(designations);
        }
        #endregion

        #region GetPDesignationById        
        [HttpGet("{PackageDesignationID}")]
        public async Task<IActionResult> GetPDesignationById(int PackageDesignationID)
        {
            var designation = await _context.PackageDestinations.FindAsync(PackageDesignationID);
            if (designation == null)
            {
                return NotFound();
            }
            return Ok(designation);
        }
        #endregion

        #region DeletePDesignationById        
        [HttpDelete("{PackageDesignationID}")]
        public async Task<IActionResult> DeletePDesignationById(int PackageDesignationID)
        {
            var designation = await _context.PackageDestinations.FindAsync(PackageDesignationID);

            if (designation == null)
            {
                return NotFound();
            }

            _context.PackageDestinations.Remove(designation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertPDesignation
        [HttpPost]
        public async Task<IActionResult> InsertPDesignation(PackageDestination designation)
        {
            await _context.PackageDestinations.AddAsync(designation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region UpdatePDesignation         
        [HttpPut("{PackageDesignationID}")]
        public async Task<IActionResult> UpdateDesignation(int PackageDesignationID, PackageDestination designation)
        {
            if (PackageDesignationID != designation.PackageDestinationId)
            {
                return BadRequest();
            }

            var existingDesignation = await _context.PackageDestinations.FindAsync(PackageDesignationID);
            if (existingDesignation == null)
            {
                return NotFound();
            }

            existingDesignation.PackageId = designation.PackageId;
            existingDesignation.DestinationId = designation.DestinationId;
            existingDesignation.OrderInTour = designation.OrderInTour;
            existingDesignation.UserId = designation.UserId;
            existingDesignation.Created = designation.Created;
            existingDesignation.Modified = designation.Modified;

            _context.PackageDestinations.Update(existingDesignation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion


        #region FilterOnPackageDestination
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PackageDestination>>> Filter([FromQuery] int? PackageID, [FromQuery] int? DestinationID)
        {
            var query = _context.PackageDestinations.Include(p => p.Package).Include(d => d.Destination).AsQueryable(); // badha int para lakhvana
                                                                                                                           //Include means User table ma jetla record 6 aene mare Package sathe map krva 6

            if (PackageID.HasValue)
                query = query.Where(c => c.PackageId == PackageID);

            if (DestinationID.HasValue)
                query = query.Where(c => c.DestinationId == DestinationID);

            return await query.ToListAsync();
        }
        #endregion

        #region PackageDropDown
        // Get all packages (for dropdown)
        [HttpGet("dropdown/Package")]
        public async Task<ActionResult<IEnumerable<object>>> GetPackages()
        {
            return await _context.MstPackages
                .Select(c => new { c.PackageId, c.PackageName })
                .ToListAsync();
        }
        #endregion

        #region DestinationDropDown
        // Get all Destination (for dropdown)
        [HttpGet("dropdown/Destination")]
        public async Task<ActionResult<IEnumerable<object>>> GetDestinations()
        {
            return await _context.MstDestinations
                .Select(c => new { c.DestinationId, c.DestinationName })
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