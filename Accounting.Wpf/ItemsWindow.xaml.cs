using System.Collections.Generic;
using System.Windows;

namespace Accounting.Wpf
{
    public partial class ItemsWindow : Window
    {
        public ItemsWindow()
        {
            InitializeComponent();

            List<ItemRow> items = new List<ItemRow>
            {
                new ItemRow { Code = "001", Name = "Свинина", Unit = "кг", Group = "Сировина" },
                new ItemRow { Code = "002", Name = "Яловичина", Unit = "кг", Group = "Сировина" },
                new ItemRow { Code = "003", Name = "Сіль", Unit = "кг", Group = "Матеріали" },
                new ItemRow { Code = "004", Name = "Оболонка", Unit = "м", Group = "Матеріали" },
                new ItemRow { Code = "005", Name = "Ящик пластиковий", Unit = "шт", Group = "Тара" },
                new ItemRow { Code = "006", Name = "Ковбаса варена", Unit = "кг", Group = "Готова продукція" }
            };

            ItemsDataGrid.ItemsSource = items;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
