using Accounting.Domain.Entities;
using Accounting.Domain.Inventory;
using Accounting.Domain.Enums;
using System;

namespace Accounting.Domain.Inventory
{
    public class InventoryMovement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int DocumentId { get; set; }
        public string DocumentType { get; set; } = "";

        public Item Item { get; set; } = new Item();
        public Warehouse Warehouse { get; set; } = new Warehouse();

        public decimal Quantity { get; set; }

        public InventoryMovementType MovementType { get; set; }

        public string Description { get; set; } = "";

        public bool Validate()
        {
            if (Date == default(DateTime))
                return false;
            if (Item == null || Item.Code == null || Item.Code == "")
                return false;
            if (Warehouse == null || Warehouse.Code == null || Warehouse.Code == "")
                return false;
            if (Quantity <= 0)
                return false;

            return true;
        }
    }
}