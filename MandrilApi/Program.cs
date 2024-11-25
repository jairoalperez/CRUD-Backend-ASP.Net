using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using MandrilApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
DotNetEnv.Env.Load();

// Read the connection string from appsettings.json
var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                          ?? throw new InvalidOperationException(Messages.Database.NoConnectionString);

// Replace environment variables on the connection string
var connectionString = ReplaceConnectionString.BuildConnectionString(rawConnectionString);

// Start the MySQL Connection (MariaDB)
builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(connectionString));

// Services and Middlewares
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