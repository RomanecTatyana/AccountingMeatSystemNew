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
        private async Task AddItemAsync()
        {
            if (!ValidateNewItem())
            {
                return;
            }

            try
            {
                ErrorMessage = "";
                StatusMessage = "Збереження номенклатури...";

                ApiClient apiClient = new ApiClient();

                CreateItemRequest request = new CreateItemRequest
                {
                    Code = NewCode.Trim(),
                    Name = NewName.Trim(),
                    FullName = NewName.Trim(),
                    Article = "",
                    Barcode = "",
                    Unit = NewUnit.Trim(),
                    GroupName = NewGroup.Trim(),
                    ItemType = "Сировина",
                    Comment = ""
                };

                var result = await apiClient.CreateItemAsync(request);

                if (!result.IsSuccess)
                {
                    ErrorMessage = result.ErrorMessage;
                    StatusMessage = "";
                    return;
                }

                NewCode = "";
                NewName = "";
                NewUnit = "";
                NewGroup = "";

                await LoadItemsAsync();

                StatusMessage = "Номенклатуру додано.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Не вдалося додати номенклатуру: {ex.Message}";
                StatusMessage = "";
            }
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
