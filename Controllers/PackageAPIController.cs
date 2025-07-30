using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourTravel.Models;

namespace TourTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageAPIController : ControllerBase
    {

        #region Configuration Fields 
        private readonly TourManagementContext _context;
        public PackageAPIController(TourManagementContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAllPackages   
        [HttpGet]
        public async Task<IActionResult> GetPackages()
        {
            // With eager loading example (if needed): Include(p => p.User)
            var packages = await _context.MstPackages.Include(u => u.User)
                .Select(c => new
                {
                    c.PackageId,
                    c.PackageCode,
                    c.PackageName,
                    c.Description,
                    c.Price,
                    c.DurationDays,
                    c.DurationNights,
                    c.AvailabilityStatus,
                    c.Category,
                    c.IncludedFeatures,
                    c.ExcludedFeatures,
                    c.ImageUrl,
                    c.CancellationPolicy,
                    c.UserId,
                    c.Created,
                    c.Modified,
                    UserName = c.User.UserName
                })
                .ToListAsync();
            return Ok(packages);
        }
        #endregion

        #region GetTop2
        [HttpGet("Top2")]
        public async Task<ActionResult<IEnumerable<MstPackage>>> GetTop2()
        {
            return await _context.MstPackages
                .Include(c => c.User)
                .Take(2)
                .ToListAsync();
        }


        #endregion

        #region GetPackageById        
        [HttpGet("{PackageID}")]
        public async Task<IActionResult> GetPackageById(int PackageID)
        {
            var package = await _context.MstPackages.FindAsync(PackageID);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }
        #endregion

        #region FilterOnPackage
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MstPackage>>> Filter([FromQuery] string? PackageName, [FromQuery] string? AvailabilityStatus, [FromQuery] int? DurationDays, [FromQuery] int? Price)
        {
            var query = _context.MstPackages.AsQueryable(); // badha int para lakhvana
            //Include means User table ma jetla record 6 aene mare Package sathe map krva 6

            if (DurationDays.HasValue)
                query = query.Where(c => c.DurationDays == DurationDays);

            if (Price.HasValue)
                query = query.Where(c => c.Price == Price);

            if (!string.IsNullOrWhiteSpace(PackageName))
            {
                query = query.Where(p => p.PackageName.Contains(PackageName));
            }
            if (!string.IsNullOrWhiteSpace(AvailabilityStatus))
            {
                query = query.Where(p => p.AvailabilityStatus.StartsWith(AvailabilityStatus));
            }
            return await query.ToListAsync();
        }
        #endregion
        //string has methods contains, startswith, endswith

        #region DeletePackageById        
        [HttpDelete("{PackageID}")]
        public async Task<IActionResult> DeletePackageById(int PackageID)
        {
            var package = await _context.MstPackages.FindAsync(PackageID);

            if (package == null)
            {
                return NotFound();
            }

            _context.MstPackages.Remove(package);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertPackages 
        [HttpPost]
        public async Task<IActionResult> InsertPackages(MstPackage package)
        {
            await _context.MstPackages.AddAsync(package);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region UpdatePackages         
        [HttpPut("{PackageID}")]
        public async Task<IActionResult> UpdatePackages(int PackageID, MstPackage package)
        {
            if (PackageID != package.PackageId)
            {
                return BadRequest();
            }

            var existingPackage = await _context.MstPackages.FindAsync(PackageID);
            if (existingPackage == null)
            {
                return NotFound();
            }

            existingPackage.PackageName = package.PackageName;
            existingPackage.Description = package.Description;
            existingPackage.Price = package.Price;
            existingPackage.DurationDays = package.DurationDays;
            existingPackage.DurationNights = package.DurationNights;
            existingPackage.AvailabilityStatus = package.AvailabilityStatus;
            existingPackage.Category = package.Category;
            existingPackage.IncludedFeatures = package.IncludedFeatures;
            existingPackage.ExcludedFeatures = package.ExcludedFeatures;
            existingPackage.ImageUrl = package.ImageUrl;
            existingPackage.CancellationPolicy = package.CancellationPolicy;
            existingPackage.UserId = package.UserId;

            _context.MstPackages.Update(existingPackage);
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

    }
}