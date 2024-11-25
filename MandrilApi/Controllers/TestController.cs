using Microsoft.AspNetCore.Mvc;

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
                return Ok("¡Conexión exitosa con la base de datos!");

            return StatusCode(500, "No se pudo conectar a la base de datos.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al conectar a la base de datos: {ex.Message}");
        }
    }
}
