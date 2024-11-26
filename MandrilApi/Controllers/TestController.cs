using Microsoft.AspNetCore.Mvc;
using MandrilApi.Helpers;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    // TEST API CONNECTION
    [HttpGet()]
    public IActionResult TestAPI()
    {
        return Ok(Messages.API.Working);
    }



    //TEST DATABASE CONNECTION
    private readonly AppDbContext _dbContext;
    public TestController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("database")]
    public async Task<IActionResult> TestDatabaseConnection()
    {
        try
        {
            var result = await _dbContext.Database.ExecuteSqlRawAsync("SELECT * FROM mandril");
            if (result == -1)
                return Ok(Messages.Database.ConnectionSuccess);

            return StatusCode(500, Messages.Database.ConnectionFailed);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error connecting with the Database: {ex.Message}");
        }
    }
}
