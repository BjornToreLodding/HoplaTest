using Microsoft.EntityFrameworkCore;
using UsersApi.Data;

var builder = WebApplication.CreateBuilder(args);
// Build the connection string dynamically from environment variables
var host = Environment.GetEnvironmentVariable("DATABASE_HOST");
var port = Environment.GetEnvironmentVariable("DATABASE_PORT");
var database = Environment.GetEnvironmentVariable("DATABASE_NAME");
var username = Environment.GetEnvironmentVariable("DATABASE_USER");
var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
// Log the environment variable values for debugging
Console.WriteLine("Environment Variables:");
Console.WriteLine($"DATABASE_HOST: {host}");
Console.WriteLine($"DATABASE_PORT: {port}");
Console.WriteLine($"DATABASE_NAME: {database}");
Console.WriteLine($"DATABASE_USER: {username}");

if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(database) || 
    string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
{
    throw new InvalidOperationException("Database environment variables are not set properly.");
}

//var connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";


// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapControllers();

app.Run();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
