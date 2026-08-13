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

                decimal amountWithoutVat = line.GetAmount();
                decimal vatAmount = line.GetVatAmount();

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
                    Amount = amountWithoutVat,
                    Description = $"Поступлення: {line.Item.Name}"
                };

                result.AccountingEntries.Add(entry);

                if (vatAmount > 0)
                {
                    AccountingEntry vatEntry = new AccountingEntry
                    {
                        Date = document.Date,
                        DocumentId = document.Id,
                        DocumentType = "ReceiptDocument",
                        DebitAccount = new Account
                        {
                            Code = "641",
                            Name = "Податковий кредит"
                        },
                        CreditAccount = new Account
                        {
                            Code = "631",
                            Name = "Розрахунки з постачальниками"
                        },
                        Amount = vatAmount,
                        Description = $"ПДВ: {line.Item.Name}"
                    };
                    result.AccountingEntries.Add(vatEntry);
                }
            }
            return result;
        }
    }
}
