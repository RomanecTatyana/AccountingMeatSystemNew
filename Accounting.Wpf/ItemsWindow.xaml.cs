using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Accounting.Wpf
{
    public partial class ItemsWindow : Window
    {
        private ObservableCollection<ItemRow> items = new ObservableCollection<ItemRow>
            {
                new ItemRow { Code = "001", Name = "Свинина", Unit = "кг", Group = "Сировина" },
                new ItemRow { Code = "002", Name = "Яловичина", Unit = "кг", Group = "Сировина" },
                new ItemRow { Code = "003", Name = "Сіль", Unit = "кг", Group = "Матеріали" },
                new ItemRow { Code = "004", Name = "Оболонка", Unit = "м", Group = "Матеріали" },
                new ItemRow { Code = "005", Name = "Ящик пластиковий", Unit = "шт", Group = "Тара" },
                new ItemRow { Code = "006", Name = "Ковбаса варена", Unit = "кг", Group = "Готова продукція" }
            };
        public ItemsWindow()
        {
            InitializeComponent();

            ItemsDataGrid.ItemsSource = items;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string code = CodeTextBox.Text.Trim();
            string name = NameTextBox.Text.Trim();
            string unit = UnitTextBox.Text.Trim();
            string group = GroupTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Вкажіть назву номенклатури.");
                return;
            }

            ItemRow newItem = new ItemRow
            {
                Code = code,
                Name = name,
                Unit = unit,
                Group = group
            };

            items.Add(newItem);

            CodeTextBox.Clear();
            NameTextBox.Clear();
            UnitTextBox.Clear();
            GroupTextBox.Clear();
        }
    }

    public class ItemRow
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string Unit { get; set; } = "";
        public string Group { get; set; } = "";
    }
}
