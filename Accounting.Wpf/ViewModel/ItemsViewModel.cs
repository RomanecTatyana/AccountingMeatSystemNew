using System.Collections.ObjectModel;
using Accounting.Domain.Entities;
using Accounting.Wpf.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;

namespace Accounting.Wpf.ViewModels
{
    public partial class ItemsViewModel : ObservableObject
    {
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

        [ObservableProperty]
        private string newCode = "";

        [ObservableProperty]
        private string newName = "";

        [ObservableProperty]
        private string newUnit = "";

        [ObservableProperty]
        private string newGroup = "";

        [ObservableProperty]
        private string statusMessage = "";

        [ObservableProperty]
        private string errorMessage = "";

        public ItemsViewModel()
        {
            _ = LoadItemsAsync();
        }

        [RelayCommand]
        private async Task LoadItemsAsync()
        {
            try
            {
                ErrorMessage = "";
                StatusMessage = "Завантаження номенклатури...";

                ApiClient apiClient = new ApiClient();

                List<Item> itemsFromApi = await apiClient.GetItemsAsync();

                Items.Clear();

                foreach (Item item in itemsFromApi)
                {
                    Items.Add(item);
                }

                StatusMessage = $"Завантажено позицій: {Items.Count}";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Не вдалося завантажити номенклатуру: {ex.Message}";
                StatusMessage = "";
            }
        }

        [RelayCommand]
        private void AddItem()
        {
            if (!ValidateNewItem())
            {
                StatusMessage = "";
                return;
            }
            Items.Add(new Item
            {
                Code = NewCode,
                Name = NewName,
                Unit = NewUnit,
                GroupName = NewGroup
            });

            NewCode = "";
            NewName = "";
            NewUnit = "";
            NewGroup = "";
            ErrorMessage = "";
            StatusMessage = "Позицію додано";
        }

        [RelayCommand]
        private void Save()
        {
            if (Items.Count == 0)
            {
                ErrorMessage = "Немає даних для збереження";
                StatusMessage = "";
                return;
            }
            else
            {
                ErrorMessage = "";
                StatusMessage = $"Збережено позицій: {Items.Count}";
            }
        }

        private bool ValidateNewItem()
        {
            if (string.IsNullOrWhiteSpace(NewCode))
            {
                ErrorMessage = "Введіть код номенклатури";
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewName))
            {
                ErrorMessage = "Введіть назву номенклатури";
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewUnit))
            {
                ErrorMessage = "Введіть одиницю виміру";
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewGroup))
            {
                ErrorMessage = "Введіть групу номенклатури";
                return false;
            }

            if (Items.Any(item => item.Code == NewCode))
            {
                ErrorMessage = "Номенклатура з таким кодом вже існує";
                return false;
            }
            return true;
        }
    }
}
