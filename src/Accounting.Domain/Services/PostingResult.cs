using System.Collections.Generic;
using Accounting.Domain.Accounting;
using Accounting.Domain.Inventory;

namespace Accounting.Domain.Services
{
    public class PostingResult
    {
        public List<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();

        public List<AccountingEntry> AccountingEntries { get; set; } = new List<AccountingEntry>();

        public List<string> Errors { get; set; } = new List<string>();

        public bool IsSuccess
        {
            get
            {
                return Errors.Count == 0;
            }
        }
    }
}