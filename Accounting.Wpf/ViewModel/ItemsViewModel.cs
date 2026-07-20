using System.Collections.ObjectModel;
using Accounting.Wpf.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Accounting.Wpf.ViewModels
{
    public partial class ItemsViewModel : ObservableObject
    {
        public ObservableCollection<ItemRow> Items { get; set; }

        [ObservableProperty]
        private string newCode = "";

        [ObservableProperty]
        private string newName = "";

        [ObservableProperty]
        private string newUnit = "";

        [ObservableProperty]
        private string newGroup = "";

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
        [RelayCommand]
        private void AddItem()
        {
            Items.Add(new ItemRow
            {
                Code = NewCode,
                Name = NewName,
                Unit = NewUnit,
                Group = NewGroup
            });

            NewCode = "";
            NewName = "";
            NewUnit = "";
            NewGroup = "";
        }
    }
}
