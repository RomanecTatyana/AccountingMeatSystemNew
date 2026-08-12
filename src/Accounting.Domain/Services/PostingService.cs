using Accounting.Domain.Accounting;
using Accounting.Domain.Entities;
using Accounting.Domain.Inventory;
using Accounting.Domain.Enums;
using System.Collections.Generic;

namespace Accounting.Domain.Services
{
    public class PostingService
    {
        public PostingResult PostReceipt(ReceiptDocument document)
        {
            PostingResult result = new PostingResult();

            if (document == null)
            {
                result.Errors.Add("Документ не передано");
                return result;
            }

            List<string> documentErrors = document.Validate();

            if (documentErrors.Count > 0)
            {
                result.Errors.AddRange(documentErrors);
                return result;
            }
            foreach (ReceiptLine line in document.Lines)
            {
                InventoryMovement movement = new InventoryMovement
                {
                    Date = document.Date,
                    DocumentId = document.Id,
                    DocumentType = "ReceiptDocument",
                    Item = line.Item,
                    Warehouse = document.Warehouse,
                    Quantity = line.Quantity,
                    MovementType = InventoryMovementType.Receipt,
                    Description = "Поступлення сировини"
                };

                result.InventoryMovements.Add(movement);

                AccountingEntry entry = new AccountingEntry
                {
                    Date = document.Date,
                    DocumentId = document.Id,
                    DocumentType = "ReceiptDocument",
                    DebitAccount = new Account
                    {
                        Code = "201",
                        Name = "Сировина і матеріали"
                    },
                    CreditAccount = new Account
                    {
                        Code = "631",
                        Name = "Розрахунки з постачальниками"
                    },
                    Amount = line.GetAmount(),
                    Description = $"Поступлення: {line.Item.Name}"
                };

                result.AccountingEntries.Add(entry);
            }
            return result;
        }
    }
}
