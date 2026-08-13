using System;
using System.Collections.Generic;
using System.Text;
using Accounting.Domain.Enums;

namespace Accounting.Domain.Entities
{
    public class ReceiptDocument
    {
        public int Id { get; set; }
        public string Number { get; set; } = "";
        public DateTime Date { get; set; }
        public Counterparty Supplier { get; set; } = new Counterparty();
        public Warehouse Warehouse { get; set; } = new Warehouse();
        public DocumentStatus Status { get; set; } = DocumentStatus.Draft;
        public List<ReceiptLine> Lines { get; set; } = new List<ReceiptLine>();
        public decimal GetTotalAmount()
        {
            decimal totalAmount = 0;

            foreach (ReceiptLine line in Lines)
            {
                totalAmount += line.GetAmount();
            }

            return totalAmount;
        }
        public decimal GetTotalQuantity()
        {
            decimal totalQuantity = 0;

            foreach (ReceiptLine line in Lines)
            {
                totalQuantity += line.Quantity;
            }

            return totalQuantity;
        }
        public decimal GetTotalVatAmount()
        {
            decimal totalVatAmount = 0;

            foreach (ReceiptLine line in Lines)
            {
                totalVatAmount += line.GetVatAmount();
            }

            return totalVatAmount;
        }

        public decimal GetTotalAmountWithVat()
        {
            return GetTotalAmount() + GetTotalVatAmount();

        }
        public List<string> Validate()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Number))
            {
                errors.Add("Не вказано номер документа");
            }

            if (Date == default)
            {
                errors.Add("Не вказано дату документа");
            }

            if (string.IsNullOrWhiteSpace(Supplier.Name))
            {
                errors.Add("Не вибрано постачальника");
            }

            if (string.IsNullOrWhiteSpace(Warehouse.Name))
            {
                errors.Add("Не вибрано склад");
            }

            if (Lines.Count == 0)
            {
                errors.Add("Документ не має жодного рядка.");
            }

            foreach (ReceiptLine line in Lines)
            {
                List<string> lineErrors = line.Validate();

                foreach (string lineError in lineErrors)
                {
                    errors.Add($"Рядок {line.Id}: {lineError}");
                }
            }

            return errors;
        }

    }
}
