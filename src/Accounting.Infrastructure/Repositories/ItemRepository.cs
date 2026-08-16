using Accounting.Domain.Entities;
using Accounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories
{
    public class ItemRepository
    {
        private readonly AppDbContext db;

        public ItemRepository(AppDbContext db)
        {
            this.db = db;
        }

        public List<Item> GetAll()
        {
            return db.Items
                .Where(item => !item.IsDeleted)
                .OrderBy(item => item.Code)
                .ToList();
        }

        public Item? GetByCode(string code)
        {
            return db.Items
                .FirstOrDefault(item => item.Code == code);
        }

        public bool ExistsByCode(string code)
        {
            return db.Items
                .Any(item => item.Code == code && !item.IsDeleted);
        }

        public void Add(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
        }
    }
}
