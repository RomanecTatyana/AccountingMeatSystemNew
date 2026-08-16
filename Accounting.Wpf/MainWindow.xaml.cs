using System.Windows;
using Accounting.Wpf.ViewModel;
using Accounting.Wpf.Views;
using Accounting.Wpf.Services;

namespace Accounting.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void OpenReceiptWindow_Click(object sender, RoutedEventArgs e)
        {
            ReceiptWindow receiptWindow = new ReceiptWindow();
            receiptWindow.Show();
        }

        private void OpenItemsWindow_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itemsWindow = new ItemsWindow();
            itemsWindow.Show();
        }
        private void OpenCounterpartiesWindow_Click(object sender, RoutedEventArgs e)
        {
            CounterpartiesWindow counterpartiesWindow = new CounterpartiesWindow();
            counterpartiesWindow.Show();
        }
        private void OpenWarehousesWindow_Click(object sender, RoutedEventArgs e)
        {
            WarehousesWindow warehousesWindow = new WarehousesWindow();
            warehousesWindow.Show();
        }

        private async void CheckApiButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ApiClient apiClient = new ApiClient();

                HealthResponse? health = await apiClient.GetHealthAsync();

                if (health == null)
                {
                    MessageBox.Show("API відповів, але відповідь порожня.");
                    return;
                }

                MessageBox.Show(
                    $"Status: {health.Status}\n" +
                    $"Service: {health.Service}\n" +
                    $"Message: {health.Message}",
                    "Перевірка API"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"API не відповідає:\n{ex.Message}",
                    "Помилка підключення до API"
                );
            }
        }
    }
}