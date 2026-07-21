using System.Windows;
using Accounting.Wpf.ViewModel;

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
    }
}