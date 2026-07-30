using System.Collections.ObjectModel;
using Accounting.Wpf.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Accounting.Wpf.ViewModel
{
    public partial class WarehousesViewModel : ObservableObject
    {
        public ObservableCollection<WarehouseRow> Warehouses { get; set; }

        [ObservableProperty]
        private string newCode = "";

        [ObservableProperty]
        private string newName = "";

        [ObservableProperty]
        private string newType = "";

        public WarehousesViewModel()
        {
            Warehouses = new ObservableCollection<WarehouseRow>
            {
                new WarehouseRow
                {
                    Code = "001",
                    Name = "Сировинний склад",
                    Type = "Сировина"
                },
                new WarehouseRow
                {
                    Code = "002",
                    Name = "Склад готової продукції",
                    Type = "Готова продукція"
                }
            };
        }
    }
}
