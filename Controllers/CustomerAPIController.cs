using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourTravel.Models;

namespace TourTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAPIController : ControllerBase
    {

        #region Configuration Fields 
        private readonly TourManagementContext _context;
        public CustomerAPIController(TourManagementContext context)
        {
            _context = context;
        }
        #endregion

//check the get all code I include User but does not select the columns is it work then there is no need to use select.

        #region GetAllCustomer       
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            var customers = await _context.MstCustomers.Include(s => s.User)
                .Select(c => new
                {
                    c.CustomerId,
                    c.FirstName,
                    c.LastName,
                    c.Email,
                    c.PhoneNumber,
                    c.Address,
                    c.DateOfBirth,
                    c.Nationality,
                    c.UserId,
                    c.Created,
                    c.Modified,
                    UserName = c.User.UserName
                })
                .ToListAsync();
            return Ok(customers);
        }
        #endregion

        #region GetCustomerById        
        [HttpGet("{CustomerID}")]
        public async Task<IActionResult> GetCustomerById(int CustomerID)
        {
            var customer = await _context.MstCustomers.FindAsync(CustomerID);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        #endregion

        #region DeleteCustomerById        
        [HttpDelete("{CustomerID}")]
        public async Task<IActionResult> DeleteCustomerById(int CustomerID)
        {
            var customer = await _context.MstCustomers.FindAsync(CustomerID);

            if (customer == null)
            {
                return NotFound();
            }

            _context.MstCustomers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertCustomer
        [HttpPost]
        public async Task<IActionResult> InsertCustomer(MstCustomer customer)
        {
            await _context.MstCustomers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region UpdateCustomer         
        [HttpPut("{CustomerID}")]
        public async Task<IActionResult> UpdateCustomer(int CustomerID, MstCustomer customer)
        {
            if (CustomerID != customer.CustomerId)
            {
                return BadRequest();
            }

            var existingCustomer = await _context.MstCustomers.FindAsync(CustomerID);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Address = customer.Address;
            existingCustomer.DateOfBirth = customer.DateOfBirth;
            existingCustomer.Nationality = customer.Nationality;
            existingCustomer.UserId = customer.UserId;
            existingCustomer.Created = customer.Created;
            existingCustomer.Modified = customer.Modified;

            _context.MstCustomers.Update(existingCustomer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region FilterOnCustomer
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MstCustomer>>> Filter([FromQuery] string? FirstName, [FromQuery] string? LastName, [FromQuery] string? Email, [FromQuery] string? Nationality)
        {
            var query = _context.MstCustomers.Include(p => p.User).AsQueryable(); // badha int para lakhvana
            //Include means User table ma jetla record 6 aene mare Package sathe map krva 6

            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                query = query.Where(p => p.FirstName.Contains(FirstName));
            }
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                query = query.Where(p => p.LastName.Contains(LastName));
            }
            if (!string.IsNullOrWhiteSpace(Email))
            {
                query = query.Where(p => p.Email.Contains(Email));
            }
            if (!string.IsNullOrWhiteSpace(Nationality))
            {
                query = query.Where(p => p.Nationality.Contains(Nationality));
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