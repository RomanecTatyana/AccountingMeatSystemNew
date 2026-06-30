using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Entities
{
    public class AccountingEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DocumentId { get; set; }
        public string DocumentType { get; set; } = "";
        public Account DebitAccount { get; set; } = new Account();
        public Account CreditAccount { get; set; } = new Account();
        public decimal Amount { get; set; }
        public string Description { get; set; } = "";

    }
}
