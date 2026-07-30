using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Wpf.Models
{
    public class CounterpartyRow
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public string TaxNumber { get; set; } = "";
    }
}
