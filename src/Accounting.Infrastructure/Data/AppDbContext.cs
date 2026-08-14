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
        }
    }
}