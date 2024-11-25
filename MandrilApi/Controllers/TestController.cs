using Microsoft.AspNetCore.Mvc;
using MandrilApi.Helpers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly DatabaseTester _tester;

    public TestController(DatabaseTester tester)
    {
        _tester = tester;
    }

    [HttpGet("database")]
    public async Task<IActionResult> TestDatabaseConnection()
    {
        try
        {
            var isConnected = await _tester.ProbarConexionAsync();

            if (isConnected)
                return Ok(Messages.Database.ConnectionSuccess);

            return StatusCode(500, Messages.Database.ConnectionFailed);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error connecting with the Database: {ex.Message}");
        }
    }
}
