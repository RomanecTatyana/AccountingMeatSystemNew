using Accounting.Domain.Entities;
using Accounting.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Repositories
{
    public class WarehouseRepository
    {
        private readonly AppDbContext db;
        public WarehouseRepository(AppDbContext db)
        {
            this.db = db;
        }

        public List<Warehouse> GetAll()
        {
            return db.Warehouses
                .Where(warehouse => !warehouse.IsDeleted)
                .OrderBy(warehouse => warehouse.Code)
                .ToList();
        }

        public Warehouse? GetByCode(string code)
        {
            return db.Warehouses
                .FirstOrDefault(warehouse => warehouse.Code == code);
        }

        public bool ExistsByCode(string code)
        {
            return db.Warehouses
                .Any(warehouse => warehouse.Code == code && !warehouse.IsDeleted);
        }

        public void Add(Warehouse warehouse)
        {
            db.Warehouses.Add(warehouse);
            db.SaveChanges();
        }

    }
}
