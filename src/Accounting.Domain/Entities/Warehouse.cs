using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string FullName { get; set; } = "";

        public string WarehouseType { get; set; } = "";

        public string Address { get; set; } = "";
        public string ResponsiblePerson { get; set; } = "";

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string Comment { get; set; } = "";
    }
}
