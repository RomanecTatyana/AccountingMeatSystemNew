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
        }
    }
}