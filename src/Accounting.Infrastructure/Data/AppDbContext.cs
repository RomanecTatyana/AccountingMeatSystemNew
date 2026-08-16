using Accounting.Domain.Accounting;
using Accounting.Domain.Entities;
using Accounting.Domain.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Counterparty> Counterparties => Set<Counterparty>();
        public DbSet<VatRate> VatRates => Set<VatRate>();
        public DbSet<UnitOfMeasure> UnitsOfMeasure => Set<UnitOfMeasure>();

        public DbSet<ReceiptDocument> ReceiptDocuments => Set<ReceiptDocument>();
        public DbSet<ReceiptLine> ReceiptLines => Set<ReceiptLine>();

        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<AccountingEntry> AccountingEntries => Set<AccountingEntry>();

        public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReceiptLine>()
                .Property(line => line.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ReceiptLine>()
                .Property(line => line.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<VatRate>()
                .Property(vatRate => vatRate.RatePercent)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AccountingEntry>()
                .Property(entry => entry.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InventoryMovement>()
                .Property(movement => movement.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    Code = "201",
                    Name = "Сировина і матеріали"
                },
                new Account
                {
                    Id = 2,
                    Code = "23",
                    Name = "Виробництво"
                },
                new Account
                {
                    Id = 3,
                    Code = "26",
                    Name = "Готова продукція"
                },
                new Account
                {
                    Id = 4,
                    Code = "361",
                    Name = "Розрахунки з покупцями"
                },
                new Account
                {
                    Id = 5,
                    Code = "631",
                    Name = "Розрахунки з постачальниками"
                },
                new Account
                {
                    Id = 6,
                    Code = "641",
                    Name = "Розрахунки за податками"
                },
                new Account
                {
                    Id = 7,
                    Code = "701",
                    Name = "Дохід від реалізації"
                },
                new Account
                {
                    Id = 8,
                    Code = "901",
                    Name = "Собівартість реалізації"
                }
            );
            modelBuilder.Entity<Item>()
                .Property(item => item.Code)
                .HasMaxLength(20);

            modelBuilder.Entity<Item>()
                .Property(item => item.Name)
                .HasMaxLength(200);

            modelBuilder.Entity<Item>()
                .Property(item => item.FullName)
                .HasMaxLength(300);

            modelBuilder.Entity<Item>()
                .Property(item => item.Article)
                .HasMaxLength(50);

            modelBuilder.Entity<Item>()
                .Property(item => item.Barcode)
                .HasMaxLength(50);

            modelBuilder.Entity<Item>()
                .Property(item => item.Unit)
                .HasMaxLength(20);

            modelBuilder.Entity<Item>()
                .Property(item => item.GroupName)
                .HasMaxLength(100);

            modelBuilder.Entity<Item>()
                .Property(item => item.ItemType)
                .HasMaxLength(100);

            modelBuilder.Entity<Warehouse>()
                .Property(warehouse => warehouse.Code)
                .HasMaxLength(20);

            modelBuilder.Entity<Warehouse>()
                .Property(warehouse => warehouse.Name)
                .HasMaxLength(200);

            modelBuilder.Entity<Warehouse>()
                .Property(warehouse => warehouse.FullName)
                .HasMaxLength(300);

            modelBuilder.Entity<Warehouse>()
                .Property(warehouse => warehouse.WarehouseType)
                .HasMaxLength(100);

            modelBuilder.Entity<Warehouse>()
                .Property(warehouse => warehouse.Address)
                .HasMaxLength(300);

            modelBuilder.Entity<Warehouse>()
                .Property(warehouse => warehouse.ResponsiblePerson)
                .HasMaxLength(150);

            modelBuilder.Entity<Counterparty>()
                .Property(counterparty => counterparty.Code)
                .HasMaxLength(20);

            modelBuilder.Entity<Counterparty>()
                .Property(counterparty => counterparty.Name)
                .HasMaxLength(200);

            modelBuilder.Entity<Counterparty>()
                .Property(counterparty => counterparty.FullName)
                .HasMaxLength(300);

            modelBuilder.Entity<Counterparty>()
                .Property(counterparty => counterparty.CounterpartyType)
                .HasMaxLength(100);

            modelBuilder.Entity<Counterparty>()
                .Property(counterparty => counterparty.TaxNumber)
                .HasMaxLength(20);

            modelBuilder.Entity<Counterparty>()
                .Property(counterparty => counterparty.VatNumber)
                .HasMaxLength(30);

            modelBuilder.Entity<Counterparty>()
                .Property(counterparty => counterparty.Phone)
                .HasMaxLength(50);

            modelBuilder.Entity<Counterparty>()
                .Property(counterparty => counterparty.Email)
                .HasMaxLength(150);

            modelBuilder.Entity<Counterparty>()
                .Property(counterparty => counterparty.LegalAddress)
                .HasMaxLength(300);

            modelBuilder.Entity<Counterparty>()
                .Property(counterparty => counterparty.ActualAddress)
                .HasMaxLength(300);
        }
    }
}