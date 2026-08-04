using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Accounting.Domain.Accounting
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

        public bool Validate()
        {
            if (Amount <= 0)
                return false;
            if (CreditAccount == null || DebitAccount == null)
                return false;
            if (Date == default(DateTime))
                return false;
            if (string.IsNullOrWhiteSpace(DebitAccount.Code))
                return false;
            if (string.IsNullOrWhiteSpace(CreditAccount.Code))
                return false;

            return true;
        }
    }

    
}
