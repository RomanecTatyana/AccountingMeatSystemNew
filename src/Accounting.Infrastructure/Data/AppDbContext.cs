using Accounting.Domain.Accounting;
using Accounting.Domain.Entities;
using Accounting.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Accounting.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }

        public DbSet<ReceiptDocument> ReceiptDocuments { get; set; }
        public DbSet<ReceiptLine> ReceiptLines { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountingEntry> AccountingEntries { get; set; }

        public DbSet<InventoryMovement> InventoryMovements { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
