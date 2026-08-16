using Accounting.Domain.Entities;
using Accounting.Infrastructure.Data;

namespace Accounting.Infrastructure.Repositories
{
    public class CounterpartyRepository
    {
        private readonly AppDbContext db;

        public CounterpartyRepository(AppDbContext db)
        {
            this.db = db;
        }

        public List<Counterparty> GetAll()
        {
            return db.Counterparties
                .Where(counterparty => !counterparty.IsDeleted)
                .OrderBy(counterparty => counterparty.Code)
                .ToList();
        }

        public Counterparty? GetByCode(string code)
        {
            return db.Counterparties
                .FirstOrDefault(counterparty => counterparty.Code == code);
        }

        public bool ExistsByCode(string code)
        {
            return db.Counterparties
                .Any(counterparty => counterparty.Code == code && !counterparty.IsDeleted);
        }

        public void Add(Counterparty counterparty)
        {
            db.Counterparties.Add(counterparty);
            db.SaveChanges();
        }
    }
}
