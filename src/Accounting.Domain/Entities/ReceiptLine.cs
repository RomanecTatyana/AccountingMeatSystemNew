using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Entities
{
    public class ReceiptLine
    {
        public int Id { get; set; }
        public Item Item { get; set; } = new Item();
        public string BatchNumber { get; set; } = "";
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public VatRate VatRate { get; set; } = new VatRate
        {
            Id = 1,
            Name = "ПДВ 20%",
            RatePercent = 20m
        };
        public decimal GetAmount()
        {
            return Quantity * Price;
        }
        public decimal GetVatAmount()
        {
            return GetAmount() * VatRate.RatePercent / 100;
        }

        public decimal GetAmountWithVat()
        {
            return GetAmount() + GetVatAmount();
        }
        public List<string> Validate()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Item.Name))
            {
                errors.Add("Не вибрана номенклатура");
            }

            if (string.IsNullOrWhiteSpace(BatchNumber))
            {
                errors.Add("Не вказано номер партії");
            }

            if (Quantity <= 0)
            {
                errors.Add("Кількість повинна бути більше нуля");
            }

            if (Price <= 0)
            {
                errors.Add("Ціна повинна бути більше нуля");
            }

            if (VatRate == null)
            {
                errors.Add("Не вибрана ставка ПДВ");
            }
            else if (VatRate.RatePercent < 0)
            {
                errors.Add("Ставка ПДВ не може бути від'ємною");
            }

            return errors;
        }
    }
}
