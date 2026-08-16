using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Entities
{
    public class Counterparty
    {
        public int Id { get; set; }

        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string FullName { get; set; } = "";

        public string CounterpartyType { get; set; } = "";

        public string TaxNumber { get; set; } = "";
        public string VatNumber { get; set; } = "";
        public bool IsVatPayer { get; set; }

        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";

        public string LegalAddress { get; set; } = "";
        public string ActualAddress { get; set; } = "";

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string Comment { get; set; } = "";
    }
}
