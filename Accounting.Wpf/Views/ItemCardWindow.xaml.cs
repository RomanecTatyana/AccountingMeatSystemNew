using System;
using System.Windows;
using System.Windows.Controls;
using Accounting.Wpf.Services;

namespace Accounting.Wpf.Views
{
    public partial class ItemCardWindow : Window
    {
        public ItemCardWindow()
        {
            InitializeComponent();

            ItemTypeComboBox.SelectedIndex = 0;
            NameTextBox.Focus();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                ErrorTextBlock.Text = "Вкажіть назву номенклатури.";
                NameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(UnitTextBox.Text))
            {
                ErrorTextBlock.Text = "Вкажіть одиницю виміру.";
                UnitTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(GroupNameTextBox.Text))
            {
                ErrorTextBlock.Text = "Вкажіть групу номенклатури.";
                GroupNameTextBox.Focus();
                return;
            }

            string itemType = "";

            if (ItemTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                itemType = selectedItem.Content?.ToString() ?? "";
            }

            if (string.IsNullOrWhiteSpace(itemType))
            {
                ErrorTextBlock.Text = "Вкажіть тип номенклатури.";
                return;
            }

            string name = NameTextBox.Text.Trim();

            CreateItemRequest request = new CreateItemRequest
            {
                Code = "",
                Name = name,
                FullName = string.IsNullOrWhiteSpace(FullNameTextBox.Text)
                    ? name
                    : FullNameTextBox.Text.Trim(),
                Article = ArticleTextBox.Text.Trim(),
                Barcode = BarcodeTextBox.Text.Trim(),
                Unit = UnitTextBox.Text.Trim(),
                GroupName = GroupNameTextBox.Text.Trim(),
                ItemType = itemType,
                Comment = CommentTextBox.Text.Trim()
            };

            try
            {
                ApiClient apiClient = new ApiClient();

                var result = await apiClient.CreateItemAsync(request);

                if (!result.IsSuccess)
                {
                    ErrorTextBlock.Text = result.ErrorMessage;
                    return;
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = $"Не вдалося зберегти номенклатуру: {ex.Message}";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
