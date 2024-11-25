using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno
DotNetEnv.Env.Load();

// Leer la cadena de conexión desde appsettings.json
var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Reemplazar variables de entorno en la cadena de conexión
var connectionString = rawConnectionString?
    .Replace("${DB_SERVER}", Environment.GetEnvironmentVariable("DB_SERVER") ?? "")
    .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT") ?? "")
    .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME") ?? "")
    .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "")
    .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "");

// Registrar el servicio MySqlConnection con la cadena de conexión interpolada
builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(connectionString));

// Configuración de servicios y middlewares
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DatabaseConnection>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();