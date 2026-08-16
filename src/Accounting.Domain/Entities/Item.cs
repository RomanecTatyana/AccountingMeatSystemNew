using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string FullName { get; set; } = "";

        public string Article { get; set; } = "";
        public string Barcode { get; set; } = "";

        public string Unit { get; set; } = "";
        public string GroupName { get; set; } = "";

        public string ItemType { get; set; } = "";

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string Comment { get; set; } = "";
    }
}
