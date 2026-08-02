using Accounting.Wpf.Models;
using Accounting.Wpf.ViewModels;
using Accounting.Wpf.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Accounting.Wpf.Views
{
    public partial class ItemsWindow : Window
    {
        
        public ItemsWindow()
        {
            InitializeComponent();

            DataContext = new ItemsViewModel();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
