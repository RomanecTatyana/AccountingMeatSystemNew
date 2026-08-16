using System.Collections.ObjectModel;
using Accounting.Wpf.Models;
using Accounting.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Accounting.Wpf.ViewModel
{
    public partial class WarehousesViewModel : ObservableObject
    {
        public ObservableCollection<Warehouse> Warehouses { get; set; }

        [ObservableProperty]
        private string newCode = "";

        [ObservableProperty]
        private string newName = "";

        [ObservableProperty]
        private string newType = "";

        public WarehousesViewModel()
        {
            Warehouses = new ObservableCollection<Warehouse>
            {
                new Warehouse
                {
                    Code = "001",
                    Name = "Сировинний склад",
                    WarehouseType = "Сировина"
                },
                new Warehouse
                {
                    Code = "002",
                    Name = "Склад готової продукції",
                    WarehouseType = "Готова продукція"
                }
            };
        }
    }
}
