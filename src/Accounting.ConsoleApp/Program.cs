using System.Globalization;
using System.Text;
using Accounting.Domain.Entities;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

List<Item> items = new List<Item>
{
    new Item
{
    Id = 1,
    Name = "Свинина",
    Unit = "кг",
    Type = "Сировина"
},

    new Item
{
    Id = 2,
    Name = "Сіль",
    Unit = "кг",
    Type = "Матеріал"
},

new Item
{
    Id = 3,
    Name = "Ковбаса",
    Unit = "кг",
    Type = "Готова продукція"
},

new Item
{
    Id = 4,
    Name = "Фарш",
    Unit = "кг",
    Type = "Готова продукція"
}
};

List<Warehouse> warehouses = new List<Warehouse>
{

new Warehouse
{
    Id = 1,
    Name = "Сировинний склад",
    Address = "Цех 1",
    ResponsiblePerson = "Комірник Іваненко"
},

new Warehouse
{
    Id = 2,
    Name = "Склад готової продукції",
    Address = "Цех 2",
    ResponsiblePerson = "Комірник Петренко"
}
};

List<Counterparty> counterparties = new List<Counterparty>
{

new Counterparty
{
    Id = 1,
    Name = "ТОВ М'ясний постачальник",
    Code = "12345678",
    Type = "Постачальник"
},

new Counterparty
{
    Id = 2,
    Name = "ТОВ Магазин №1",
    Code = "87654321",
    Type = "Покупець"
}};
List<Account> accounts = new List<Account>
{
    new Account
    {
        Id = 1,
        Code = "201",
        Name = "Сировина і матеріали",
        Type = "Активний"
    },
    new Account
    {
        Id = 2,
        Code = "23",
        Name = "Виробництво",
        Type = "Активний"
    },
    new Account
    {
        Id = 3,
        Code = "26",
        Name = "Готова продукція",
        Type = "Активний"
    },
    new Account
    {
        Id = 4,
        Code = "631",
        Name = "Розрахунки з постачальниками",
        Type = "Пасивний"
    },
    new Account
    {
        Id = 5,
        Code = "641",
        Name = "Розрахунки за податками",
        Type = "Активно-пасивний"
    },
    new Account
    {
        Id = 6,
        Code = "701",
        Name = "Дохід від реалізації",
        Type = "Активний"
    },
    new Account
    {
        Id = 7,
        Code = "901",
        Name = "Собівартість реалізації",
        Type = "Пасивний"
    },
};

List<VatRate> vatRates = new List<VatRate>
{
    new VatRate
    {
        Id = 1,
        Name = "ПДВ 20%",
        RatePercent = 20m
    },
    new VatRate
    {
        Id = 2,
        Name = "ПДВ 7%",
        RatePercent = 7m
    },
    new VatRate
    {
        Id = 3,
        Name = "ПДВ 0%",
        RatePercent = 0m
    },
    new VatRate
    {
        Id = 4,
        Name = "Без ПДВ",
        RatePercent = 0m
    },
};

List<UnitOfMeasure> units = new List<UnitOfMeasure>
{
    new UnitOfMeasure
    {
        Id = 1,
        Code = "кг",
        Name = "Кілограм"
    },
    new UnitOfMeasure
    {
        Id = 2,
        Code = "шт",
        Name = "Штука"
    },
    new UnitOfMeasure
    {
        Id = 3,
        Code = "л",
        Name = "Літр"
    },
    new UnitOfMeasure
    {
        Id = 4,
        Code = "м",
        Name = "Метр"
    },
};

List<ReceiptDocument> receiptDocuments = new List<ReceiptDocument>();

while (true)
{
    Console.WriteLine("=== Accounting Meat System Console ===");
    Console.WriteLine();
    Console.WriteLine("1. Показати номенклатуру");
    Console.WriteLine("2. Додати номенклатуру");
    Console.WriteLine("3. Показати склади");
    Console.WriteLine("4. Показати контрагентів");
    Console.WriteLine("5. Порахувати суму надходження");
    Console.WriteLine("6. Показати рахунки обліку");
    Console.WriteLine("7. Показати ставки ПДВ");
    Console.WriteLine("8. Показати одиниці виміру");
    Console.WriteLine("9. Показати всі довідники");
    Console.WriteLine("10. Створити надходження сировини");
    Console.WriteLine("11. Показати документи надходження сировини");
    Console.WriteLine("0. Вийти");
    Console.WriteLine();

    Console.Write("Оберіть дію:");
    string choice = Console.ReadLine()!;

    Console.WriteLine();

    if (choice == "1")
    {
        Console.WriteLine("=== Номенклатура ===");

        foreach (Item item in items)
        {
            Console.WriteLine($"{item.Id}. {item.Name}, {item.Unit}, {item.Type}");
        }
    }
    else if (choice == "2")
    {
        Console.WriteLine("=== Додавання номенклатури ===");

        Console.Write("Назва: ");
        string name = Console.ReadLine()!;

        Console.Write("Одиниця виміру: ");
        string unit = Console.ReadLine()!;

        Console.Write("Тип: ");
        string type = Console.ReadLine()!;

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Назва не може бути порожньою.");
        }
        else if (string.IsNullOrEmpty (unit)) 
        {
            Console.WriteLine("Одиниця виміру не може бути порожньою.");
        }
        else if (string.IsNullOrEmpty(type))
        {
            Console.WriteLine("Тип не може бути порожнім.");
        }
        else
        {
            bool alreadyExists = false;

            foreach (Item item in items)
            {
                if (string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    alreadyExists = true;
                }
            }

            if (alreadyExists)
            {
                Console.WriteLine("Така номенклатура вже є в довіднику.");
            }
            else
            {
                int nextId = items.Count + 1;

                Item newItem = new Item
                {
                    Id = nextId,
                    Name = name,
                    Unit = unit,
                    Type = type
                };

                items.Add(newItem);

                Console.WriteLine("Номенклатуру додано");
            }
        }
    }
    else if (choice == "3")
    {
        Console.WriteLine("=== Склади ===");

        foreach (Warehouse warehouse in warehouses)
        {
            Console.WriteLine($"{warehouse.Id}, {warehouse.Name}, {warehouse.Address}, {warehouse.ResponsiblePerson}");
        }
    }
    else if (choice == "4")
    {
        Console.WriteLine("=== Контрагенти ===");

        foreach (Counterparty counterparty in counterparties)
        {
            Console.WriteLine($"{counterparty.Id}. {counterparty.Name}, {counterparty.Code}, {counterparty.Type}");
        }
    }
    else if (choice == "5")
    {
        Console.WriteLine("=== Розрахунок суми надходження ===");

        Console.WriteLine("Оберіть номенклатуру:");

        foreach (Item item in items)
        {
            Console.WriteLine($"{item.Id}. {item.Name}, {item.Unit}, {item.Type}");
        }

        Console.WriteLine();

        Console.Write("Введіть Id номенклатури: ");
        int itemId = int.Parse(Console.ReadLine()!);

        Item? selectedItem = null;

        foreach (Item item in items)
        {
            if (item.Id == itemId)
            {
                selectedItem = item;
            }
        }

        if (selectedItem == null)
        {
            Console.WriteLine("Номенклатуру з таким Id не знайдено.");
        }
        else
        {
            Console.Write("Введіть кількість: ");
            decimal quantity = decimal.Parse(
                Console.ReadLine()!.Replace(",", "."),
                CultureInfo.InvariantCulture
            );

            Console.Write("Введіть ціну: ");
            decimal price = decimal.Parse(
                Console.ReadLine()!.Replace(",", "."),
                CultureInfo.InvariantCulture
            );

            if (quantity <= 0)
            {
                Console.WriteLine("Кількість повинна бути більше нуля.");
            }
            else if (price <= 0)
            {
                Console.WriteLine("Ціна повинна бути більше нуля.");
            }
            else
            {
                decimal amountWithoutVat = quantity * price;

                VatRate selectedVatRate = vatRates[0];

                decimal vatAmount = amountWithoutVat * selectedVatRate.RatePercent / 100;
                decimal amountWithVat = amountWithoutVat + vatAmount;

                Console.WriteLine();
                Console.WriteLine("=== Результат ===");
                Console.WriteLine($"Номенклатура: {selectedItem.Name}");
                Console.WriteLine($"Кількість: {quantity} {selectedItem.Unit}");
                Console.WriteLine($"Ціна без ПДВ: {price}");
                Console.WriteLine($"Сума без ПДВ: {amountWithoutVat}");
                Console.WriteLine($"Ставка ПДВ: {selectedVatRate.RatePercent}%");
                Console.WriteLine($"Сума ПДВ: {vatAmount}");
                Console.WriteLine($"Сума з ПДВ: {amountWithVat}");
            }
        }
    }
    else if (choice == "6")
    {
        Console.WriteLine("=== Рахунки обліку ===");

        foreach (Account account in accounts)
        {
            Console.WriteLine($"{account.Id}. {account.Code} — {account.Name}, {account.Type}");
        }
    }
    else if (choice == "7")
    {
        Console.WriteLine("=== Ставки ПДВ ===");

        foreach (VatRate vatRate in vatRates)
        {
            Console.WriteLine($"{vatRate.Id}. {vatRate.Name}: {vatRate.RatePercent}%");
        }
    }
    else if (choice == "8")
    {
        Console.WriteLine("=== Одиниці виміру ===");

        foreach (UnitOfMeasure unit in units)
        {
            Console.WriteLine($"{unit.Id}. {unit.Code} - {unit.Name}");
        }
    }
    else if (choice == "9")
    {
        Console.WriteLine("=== Номенклатура ===");

        foreach (Item item in items)
        {
            Console.WriteLine($"{item.Id}. {item.Name}, {item.Unit}, {item.Type}");
        };
        Console.WriteLine();
        Console.WriteLine("=== Склади ===");

        foreach (Warehouse warehouse in warehouses)
        {
            Console.WriteLine($"{warehouse.Id}, {warehouse.Name}, {warehouse.Address}, {warehouse.ResponsiblePerson}");
        };
        Console.WriteLine();
        Console.WriteLine("=== Контрагенти ===");

        foreach (Counterparty counterparty in counterparties)
        {
            Console.WriteLine($"{counterparty.Id}. {counterparty.Name}, {counterparty.Code}, {counterparty.Type}");
        };
        Console.WriteLine();
        Console.WriteLine("=== Рахунки обліку ===");

        foreach (Account account in accounts)
        {
            Console.WriteLine($"{account.Id}. {account.Code} — {account.Name}, {account.Type}");
        };
        Console.WriteLine();
        Console.WriteLine("=== Ставки ПДВ ===");

        foreach (VatRate vatRate in vatRates)
        {
            Console.WriteLine($"{vatRate.Id}. {vatRate.Name}: {vatRate.RatePercent}%");
        };
        Console.WriteLine();
        Console.WriteLine("=== Одиниці виміру ===");

        foreach (UnitOfMeasure unit in units)
        {
            Console.WriteLine($"{unit.Id}. {unit.Code} - {unit.Name}");
        };
    }
    else if (choice == "10")
    {
        Console.WriteLine("=== Створення документа надходження сировини ===");

        int nextId = receiptDocuments.Count + 1;
        string number = $"НС-{nextId:000000}";

        Console.WriteLine($"Номер документа: {number}");

        Console.Write("Введіть дату документа у форматі дд.мм.рррр: ");
        DateTime date = DateTime.ParseExact(
            Console.ReadLine()!,
            "dd.MM.yyyy",
            CultureInfo.InvariantCulture
        );

        Console.WriteLine($"Дата документа: {date:dd.MM.yyyy}");
        Console.WriteLine();

        Console.WriteLine("Оберіть постачальника: ");

        foreach (Counterparty counterparty in counterparties)
        {
            Console.WriteLine($"{counterparty.Id}. {counterparty.Name}, {counterparty.Code}, {counterparty.Type}");
        }

        int supplierId = int.Parse(Console.ReadLine()!);

        Counterparty? selectedSupplier = null;

        foreach (Counterparty counteraparty in counterparties)
        {
            if (counteraparty.Id == supplierId)
            {
                selectedSupplier = counteraparty;
            }
        }

        if (selectedSupplier == null)
        {
            Console.WriteLine("Постачальника з таким Id не знайдено.");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Оберіть склад: ");

            foreach (Warehouse warehouse in warehouses)
            {
                Console.WriteLine($"{warehouse.Id}. {warehouse.Name}, {warehouse.Address}, {warehouse.ResponsiblePerson}");
            }

            int warehouseId = int.Parse(Console.ReadLine()!);

            Warehouse? selectedWarehouse = null;

            foreach (Warehouse warehouse in warehouses)
            {
                if (warehouse.Id == warehouseId)
                {
                    selectedWarehouse = warehouse;
                }
            }

            if (selectedWarehouse == null)
            {
                Console.WriteLine("Склад з таким Id не знайдено.");
            }
            else
            {
                ReceiptDocument receiptDocument = new ReceiptDocument
                {
                    Id = nextId,
                    Number = number,
                    Date = date,
                    Supplier = selectedSupplier,
                    Warehouse = selectedWarehouse
                };

                receiptDocuments.Add(receiptDocument);

                Console.WriteLine();
                Console.WriteLine("Документ надходження сировини створено.");
                Console.WriteLine($"Номер: {receiptDocument.Number}");
                Console.WriteLine($"Дата: {receiptDocument.Date:dd.MM.yyyy}");
                Console.WriteLine($"Постачальник: {receiptDocument.Supplier.Name}");
                Console.WriteLine($"Склад: {receiptDocument.Warehouse.Name}");
            }
        }
    }

    else if (choice == "11")
    {
        Console.WriteLine("=== Надходження сировини ===");

        if (receiptDocuments.Count == 0)
        {
            Console.WriteLine("Документів поки немає.");
        }
        else
        {
            foreach (ReceiptDocument document in receiptDocuments)
            {
                Console.WriteLine($"{document.Id}. {document.Number} від {document.Date:dd.MM.yyyy}");
                Console.WriteLine($"   Постачальник: {document.Supplier.Name}");
                Console.WriteLine($"   Склад: {document.Warehouse.Name}");
                Console.WriteLine();
            }
        }
    }
    else if (choice == "0")
    {
        Console.WriteLine("Вихід з програми.");
        break;
    }
    else
    {
        Console.WriteLine("Невідома команда.");
    }

    Console.WriteLine();
    Console.WriteLine("Натисніть Enter, щоб продовжити...");
    Console.ReadLine();
    Console.WriteLine();
}
           