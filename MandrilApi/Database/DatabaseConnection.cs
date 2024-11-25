using MySqlConnector;

public class DatabaseConnection
{
    private readonly MySqlConnection _connection;

    public DatabaseConnection(MySqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> ProbarConexionAsync()
    {
        try
        {
            string query = "SELECT 1";

            await _connection.OpenAsync(); // Open Connection

            using var command = new MySqlCommand(query, _connection);
            var result = await command.ExecuteScalarAsync(); // Ejecuta el query y obtiene el resultado

            await _connection.CloseAsync(); // Cierra la conexión

            return result != null && Convert.ToInt32(result) == 1; // Verifica el resultado
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error de conexión: {ex.Message}"); // Agrega un log
            throw;
        }
    }
}
