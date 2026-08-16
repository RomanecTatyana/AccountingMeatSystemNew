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
builder.Services.AddScoped<CounterpartyRepository>();

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

app.MapPost("/api/items", (CreateItemRequest request, ItemRepository itemRepository) =>
{
    string code = string.IsNullOrWhiteSpace(request.Code)
    ? itemRepository.GetNextCode()
    : request.Code.Trim();

    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest("Назва номенклатури не може бути порожньою.");
    }

    if (string.IsNullOrWhiteSpace(request.Unit))
    {
        return Results.BadRequest("Одиниця виміру не може бути порожньою.");
    }

    if (string.IsNullOrWhiteSpace(request.GroupName))
    {
        return Results.BadRequest("Група номенклатури не може бути порожньою.");
    }

    bool alreadyExists = itemRepository.ExistsByCode(code);

    if (alreadyExists)
    {
        return Results.Conflict("Номенклатура з таким кодом вже існує.");
    }

    Item item = new Item
    {
        Code = code,
        Name = request.Name.Trim(),
        FullName = request.FullName.Trim(),
        Article = request.Article.Trim(),
        Barcode = request.Barcode.Trim(),
        Unit = request.Unit.Trim(),
        GroupName = request.GroupName.Trim(),
        ItemType = request.ItemType.Trim(),
        Comment = request.Comment.Trim(),
        IsActive = true,
        IsDeleted = false
    };

    itemRepository.Add(item);

    return Results.Created($"/api/items/{item.Id}", item);
});

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

    if (string.IsNullOrWhiteSpace(request.WarehouseType))
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
        FullName = request.FullName.Trim(),
        WarehouseType = request.WarehouseType.Trim(),
        Address = request.Address.Trim(),
        ResponsiblePerson = request.ResponsiblePerson.Trim(),
        Comment = request.Comment.Trim()
    };

    warehouseRepository.Add(warehouse);

    return Results.Created($"/api/warehouses/{warehouse.Id}", warehouse);
});

app.MapGet("/api/counterparties", (CounterpartyRepository counterpartyRepository) =>
{
    List<Counterparty> counterparties = counterpartyRepository.GetAll();

    return Results.Ok(counterparties);
});


app.MapPost("/api/counterparties", (CreateCounterpartyRequest request, CounterpartyRepository counterpartyRepository) =>
{
    if (string.IsNullOrWhiteSpace(request.Code))
    {
        return Results.BadRequest("Код контрагента не може бути порожнім.");
    }

    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest("Назва контрагента не може бути порожньою.");
    }

    if (string.IsNullOrWhiteSpace(request.CounterpartyType))
    {
        return Results.BadRequest("Тип контрагента не може бути порожнім.");
    }

    if (string.IsNullOrWhiteSpace(request.TaxNumber))
    {
        return Results.BadRequest("Податковий номер не може бути порожнім.");
    }

    bool alreadyExists = counterpartyRepository.ExistsByCode(request.Code.Trim());

    if (alreadyExists)
    {
        return Results.Conflict("Контрагент з таким кодом вже існує.");
    }

    Counterparty counterparty = new Counterparty
    {
        Code = request.Code.Trim(),
        Name = request.Name.Trim(),
        FullName = request.FullName.Trim(),
        CounterpartyType = request.CounterpartyType.Trim(),
        TaxNumber = request.TaxNumber.Trim(),
        VatNumber = request.VatNumber.Trim(),
        IsVatPayer = request.IsVatPayer,
        Phone = request.Phone.Trim(),
        Email = request.Email.Trim(),
        LegalAddress = request.LegalAddress.Trim(),
        ActualAddress = request.ActualAddress.Trim(),
        Comment = request.Comment.Trim()
    };

    counterpartyRepository.Add(counterparty);

    return Results.Created($"/api/counterparties/{counterparty.Id}", counterparty);
});

app.Run();

record CreateItemRequest(
    string Code,
    string Name,
    string FullName,
    string Article,
    string Barcode,
    string Unit,
    string GroupName,
    string ItemType,
    string Comment
);
record CreateWarehouseRequest(
    string Code,
    string Name,
    string FullName,
    string WarehouseType,
    string Address,
    string ResponsiblePerson,
    string Comment
);
record CreateCounterpartyRequest(
    string Code,
    string Name,
    string FullName,
    string CounterpartyType,
    string TaxNumber,
    string VatNumber,
    bool IsVatPayer,
    string Phone,
    string Email,
    string LegalAddress,
    string ActualAddress,
    string Comment
);