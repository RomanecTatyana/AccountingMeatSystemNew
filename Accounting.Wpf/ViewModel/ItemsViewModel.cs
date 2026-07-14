using System.Collections.ObjectModel;
using Accounting.Wpf.Models;

namespace Accounting.Wpf.ViewModels
{
    public class ItemsViewModel
    {
        public ObservableCollection<ItemRow> Items { get; set; }

        public ItemsViewModel()
        {
            Items = new ObservableCollection<ItemRow>
            {
                new ItemRow
                {
                    Code = "001",
                    Name = "Свинина",
                    Unit = "кг",
                    Group = "Сировина"
                },
                new ItemRow
                {
                    Code = "002",
                    Name = "Яловичина",
                    Unit = "кг",
                    Group = "Сировина"
                },
                new ItemRow
                {
                    Code = "003",
                    Name = "Сіль",
                    Unit = "кг",
                    Group = "Матеріали"
                }
            };
        }
    }
}
