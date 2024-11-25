using Microsoft.EntityFrameworkCore;

public class DatabaseTester
{
    private readonly AppDbContext _dbContext;

    public DatabaseTester(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ProbarConexionAsync()
    {
        try
        {
            // Ejecuta una consulta básica para verificar la conexión
            var result = await _dbContext.Database.ExecuteSqlRawAsync("SELECT 1");
            return result == -1; // ExecuteSqlRawAsync devuelve -1 si se ejecutó con éxito.
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al probar la conexión: {ex.Message}");
            throw;
        }
    }
}
