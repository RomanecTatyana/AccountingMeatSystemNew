using Accounting.Domain.Entities;
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
builder.Services.AddScoped<WarehouseRepository>();

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


app.MapGet("/api/items", (ItemRepository itemRepository) =>
{
    List<Item> items = itemRepository.GetAll();
    return Results.Ok(items);
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPost("/api/items", (CreateItemRequest request, ItemRepository itemRepository) =>
{
    if (string.IsNullOrWhiteSpace(request.Code))
    {
        return Results.BadRequest("Код номенклатури не може бути порожнім.");
    }

    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest("Назва номенклатури не може бути порожньою.");
    }

    if (string.IsNullOrWhiteSpace(request.Unit))
    {
        return Results.BadRequest("Одиниця виміру не може бути порожньою.");
    }

    if (string.IsNullOrWhiteSpace(request.Group))
    {
        return Results.BadRequest("Група номенклатури не може бути порожньою.");
    }

    bool alreadyExists = itemRepository.ExistsByCode(request.Code.Trim());

    if (alreadyExists)
    {
        return Results.Conflict("Номенклатура з таким кодом вже існує.");
    }

    Item item = new Item
    {
        Code = request.Code.Trim(),
        Name = request.Name.Trim(),
        Unit = request.Unit.Trim(),
        Group = request.Group.Trim()
    };

    itemRepository.Add(item);

    return Results.Created($"/api/items/{item.Id}", item);
});

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

app.MapGet("/api/warehouses", (WarehouseRepository warehouseRepository) =>
{
    List<Warehouse> warehouses = warehouseRepository.GetAll();

    return Results.Ok(warehouses);
});

app.MapPost("/api/warehouses", (CreateWarehouseRequest request, WarehouseRepository warehouseRepository) =>
{
    if (string.IsNullOrWhiteSpace(request.Code))
    {
        return Results.BadRequest("Код складу не може бути порожнім.");
    }

    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest("Назва складу не може бути порожньою.");
    }

    if (string.IsNullOrWhiteSpace(request.Type))
    {
        return Results.BadRequest("Тип складу не може бути порожнім.");
    }

    bool alreadyExists = warehouseRepository.ExistsByCode(request.Code.Trim());

    if (alreadyExists)
    {
        return Results.Conflict("Склад з таким кодом вже існує.");
    }

    Warehouse warehouse = new Warehouse
    {
        Code = request.Code.Trim(),
        Name = request.Name.Trim(),
        Type = request.Type.Trim()
    };

    warehouseRepository.Add(warehouse);

    return Results.Created($"/api/warehouses/{warehouse.Id}", warehouse);
});
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record CreateItemRequest(string Code, string Name, string Unit, string Group);
record CreateWarehouseRequest(string Code, string Name, string Type);