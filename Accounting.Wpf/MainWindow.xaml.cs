using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Accounting.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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