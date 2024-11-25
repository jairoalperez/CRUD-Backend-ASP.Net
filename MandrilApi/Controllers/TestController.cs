using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly DatabaseConnection _repository;

    public TestController(DatabaseConnection repository)
    {
        _repository = repository;
    }

    [HttpGet("test-connection")]
    public async Task<IActionResult> TestConnection()
    {
        var isConnected = await _repository.ProbarConexionAsync();

        if (isConnected)
        {
            return Ok("¡Conexión exitosa con la base de datos!");
        }

        return StatusCode(500, "No se pudo conectar a la base de datos.");
    }
}
