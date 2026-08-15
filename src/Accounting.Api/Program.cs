using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string? connectionString = Environment.GetEnvironmentVariable("ACCOUNTING_MEAT_CONNECTION_STRING");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Environment variable ACCOUNTING_MEAT_CONNECTION_STRING is not set."
    );
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapGet("/api/health", () =>
{
    return Results.Ok(new
    {
        Status = "OK",
        Service = "Accounting.Api",
        Message = "Accounting API is running"
    });
});

app.MapGet("/api/health/db", async (AppDbContext db) =>
{
    bool canConnect = await db.Database.CanConnectAsync();

    return Results.Ok(new
    {
        Status = canConnect ? "OK" : "ERROR",
        Database = "accounting_meat_dev",
        CanConnect = canConnect
    });
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
