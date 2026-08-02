using System.Collections.ObjectModel;
using Accounting.Wpf.Models;
using Accounting.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Accounting.Wpf.ViewModel
{
    public partial class CounterpartiesViewModel : ObservableObject
    {
        public ObservableCollection<Counterparty> Counterparties { get; set; }

        [ObservableProperty]
        private string newCode = "";

        [ObservableProperty]
        private string newName = "";

        [ObservableProperty]
        private string newType = "";

        public CounterpartiesViewModel()
        {
            Counterparties = new ObservableCollection<Counterparty>
            {
                new Counterparty
                {
                    Code = "001",
                    Name = "ТОВ 'Сонячна енергія'",
                    Type = "Постачальник",
                    TaxNumber = "1234567890"
                },
                new Counterparty
                {
                    Code = "002",
                    Name = "ТОВ 'Електроенергія плюс'",
                    Type = "Постачальник",
                    TaxNumber = "0987654321"
                },
                new Counterparty
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
