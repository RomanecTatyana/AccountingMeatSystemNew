using Accounting.Wpf.Models;
using Accounting.Wpf.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Accounting.Wpf
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ItemsViewModel viewModel)
            {
                viewModel.Items.Add(new ItemRow
                {
                    Code = CodeTextBox.Text,
                    Name = NameTextBox.Text,
                    Unit = UnitTextBox.Text,
                    Group = GroupTextBox.Text
                });

                CodeTextBox.Clear();
                NameTextBox.Clear();
                UnitTextBox.Clear();
                GroupTextBox.Clear();
            }
        }
    }
}
