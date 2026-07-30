using Accounting.Wpf.ViewModel;
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
    /// Interaction logic for CounterpartiesWindow.xaml
    /// </summary>
    public partial class CounterpartiesWindow : Window
    {
        public CounterpartiesWindow()
        {
            InitializeComponent();
            DataContext = new CounterpartiesViewModel();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
