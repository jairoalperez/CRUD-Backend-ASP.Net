using MandrilApi.Database;
using Microsoft.EntityFrameworkCore;
using MandrilApi.Helpers;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Get and prepare the connection string
var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                          ?? throw new InvalidOperationException(Messages.Database.NoConnectionString);
var connectionString = ReplaceConnectionString.BuildConnectionString(rawConnectionString);

// Configuration of EF Core with MariaDB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(10, 5, 0))
    )
);


// Services and middlewares
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DatabaseTester>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
