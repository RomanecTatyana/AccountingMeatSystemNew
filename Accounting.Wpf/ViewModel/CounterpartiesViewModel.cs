using System.Collections.ObjectModel;
using Accounting.Wpf.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Accounting.Wpf.ViewModel
{
    public partial class CounterpartiesViewModel : ObservableObject
    {
        public ObservableCollection<CounterpartyRow> Counterparties { get; set; }

        [ObservableProperty]
        private string newCode = "";

        [ObservableProperty]
        private string newName = "";

        [ObservableProperty]
        private string newType = "";

        public CounterpartiesViewModel()
        {
            Counterparties = new ObservableCollection<CounterpartyRow>
            {
                new CounterpartyRow
                {
                    Code = "001",
                    Name = "ТОВ 'Сонячна енергія'",
                    Type = "Постачальник",
                    TaxNumber = "1234567890"
                },
                new CounterpartyRow
                {
                    Code = "002",
                    Name = "ТОВ 'Електроенергія плюс'",
                    Type = "Постачальник",
                    TaxNumber = "0987654321"
                },
                new CounterpartyRow
                {
                    Code = "003",
                    Name = "ТОВ 'Будівельні матеріали'",
                    Type = "Покупець",
                    TaxNumber = "1122334455"
                }
            };
        }
    }
}
