using System.Windows;
using Accounting.Wpf.ViewModel;
using Accounting.Wpf.Views;

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
    }
}