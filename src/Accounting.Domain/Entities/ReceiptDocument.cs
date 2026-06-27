using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Entities
{
    public class ReceiptDocument
    {
        public int Id { get; set; }
        public string Number { get; set; } = "";
        public DateTime Date { get; set; }
        public Counterparty Supplier { get; set; } = new Counterparty();
        public Warehouse Warehouse { get; set; } = new Warehouse();
        public List<ReceiptLine> Lines { get; set; } = new List<ReceiptLine>();
    }
}
