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
        public decimal Amount { get; set; }
    }
}
