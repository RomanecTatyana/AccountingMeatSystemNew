using Accounting.Wpf.ViewModel;
using Accounting.Wpf.ViewModels;
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
    /// Interaction logic for WarehousesWindow.xaml
    /// </summary>
    public partial class WarehousesWindow : Window
    {
        public WarehousesWindow()
        {
            InitializeComponent();
            DataContext = new WarehousesViewModel();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
