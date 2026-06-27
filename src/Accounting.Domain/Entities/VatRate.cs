using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Entities
{
    public class VatRate
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal RatePercent { get; set; }
    }
}
