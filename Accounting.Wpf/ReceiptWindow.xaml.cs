using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Accounting.Wpf
{
    /// <summary>
    /// Interaction logic for ReceiptWindow.xaml
    /// </summary>
    public partial class ReceiptWindow : Window
    {
        public ReceiptWindow()
        {
            InitializeComponent();

            ReceiptDraft receiptDraft = new ReceiptDraft
            {
                DocumentDate = DateTime.Today,
                Number = "ВХ-0001",
                SupplierName = "Тестовий постачальник",
                WarehouseName = "Сировинний склад",
                Comment = "Навчальний документ"
            };

            DataContext = receiptDraft;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Збереження документа буде додано пізніше.");
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Проведення документа буде додано пізніше.");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class ReceiptDraft
    {
        public string Number { get; set; } = "";
        public DateTime DocumentDate { get; set; }
        public string SupplierName { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string Comment { get; set; } = "";
    }
}
