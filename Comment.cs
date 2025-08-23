//namespace TourTravel
//{
//    public class Comment
//    {
//    }
//}

//#region TotalCustomer       
//[HttpGet("CustomerCount")]
//public async Task<IActionResult> CustomerCount()
//{
//    var customerCount = await _context.MstCustomers.CountAsync();
//    return Ok(new { Count = customerCount });
//}
//#endregion

//In Program.cs For Validation

//builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); -> for Another package without aspnet
//builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>(); -> For Manualy Apply Validations on Single - Single table