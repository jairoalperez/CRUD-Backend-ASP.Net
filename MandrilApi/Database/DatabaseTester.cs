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
            // Basic query to check connection
            var result = await _dbContext.Database.ExecuteSqlRawAsync("SELECT 1");
            return result == -1; // ExecuteSqlRawAsync returns -1 if it succeded
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error testing connection: {ex.Message}");
            throw;
        }
    }
}
