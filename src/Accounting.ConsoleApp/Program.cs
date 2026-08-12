using System.Globalization;
using System.Text;
using Accounting.Domain.Accounting;
using Accounting.Domain.Entities;
using Accounting.Domain.Enums;
using Accounting.Domain.Inventory;
using Accounting.Domain.Services;


Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

//Довідники

List<Item> items = new List<Item>
{
    new Item
{
    Code = "001",
    Name = "Свинина",
    Unit = "кг",
    Group = "Сировина"
},

    new Item
{
    Code = "002",
    Name = "Сіль",
    Unit = "кг",
    Group = "Матеріали"
},

new Item
{
    Code = "003",
    Name = "Ковбаса",
    Unit = "кг",
    Group = "Готова продукція"
},

new Item
{
    Code = "004",
    Name = "Фарш",
    Unit = "кг",
    Group = "Готова продукція"
}
};

List<Warehouse> warehouses = new List<Warehouse>
{

new Warehouse
{
    Code = "001",
    Name = "Сировинний склад",
    Type = "Цех 1"
},

new Warehouse
{
    Code = "002",
    Name = "Склад готової продукції",
    Type = "Цех 2"
}
};

List<Counterparty> counterparties = new List<Counterparty>
{

new Counterparty
{
    Code = "001",
    Name = "ТОВ М'ясний постачальник",
    Type = "Постачальник",
    TaxNumber = "87654321",
},

new Counterparty
{
    Code = "002",
    Name = "ТОВ Магазин №1",
    Type = "Покупець",
    TaxNumber = "87654321"
}};

List<Account> accounts = new List<Account>
{
    new Account
    {
        Code = "201",
        Name = "Сировина і матеріали"
    },
    new Account
    {
        Code = "23",
        Name = "Виробництво"
    },
    new Account
    {
        Code = "26",
        Name = "Готова продукція"
    },
    new Account
    {
        Code = "631",
        Name = "Розрахунки з постачальниками"
    },
    new Account
    {
        Code = "641",
        Name = "Розрахунки за податками"
    },
    new Account
    {
        Code = "701",
        Name = "Дохід від реалізації"
    },
    new Account
    {
        Code = "901",
        Name = "Собівартість реалізації"
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

//Регістри

List<ReceiptDocument> receiptDocuments = new List<ReceiptDocument>();

List<AccountingEntry> accountingEntries = new List<AccountingEntry>();

List<InventoryMovement> inventoryMovements = new List<InventoryMovement>();

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
    Console.WriteLine("12. Додати рядок в документ надходження");
    Console.WriteLine("13. Показати підсумок надходження");
    Console.WriteLine("14. Перевірити надходження");
    Console.WriteLine("15. Змінити статус надходження на Posted");
    Console.WriteLine("16. Скасувати документ надходження");
    Console.WriteLine("17. Показати проводки");
    Console.WriteLine("0 Вийти");
    Console.WriteLine();

    Console.Write("Оберіть дію:");
    string choice = Console.ReadLine()!;

    Console.WriteLine();

    if (choice == "1")
    {
        Console.WriteLine("=== Номенклатура ===");

        foreach (Item item in items)
        {
            Console.WriteLine($"{item.Code}. {item.Name}, {item.Unit}, {item.Group}");
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
                Item newItem = new Item
                {
                    Code = "1",
                    Name = name,
                    Unit = unit,
                    Group = type
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
            Console.WriteLine($"{warehouse.Code}, {warehouse.Name}, {warehouse.Type}");
        }
    }
    else if (choice == "4")
    {
        Console.WriteLine("=== Контрагенти ===");

        foreach (Counterparty counterparty in counterparties)
        {
            Console.WriteLine($"{counterparty.Code}. {counterparty.Name}, {counterparty.Type}, {counterparty.TaxNumber}");
        }
    }
    else if (choice == "5")
    {
        Console.WriteLine("=== Розрахунок суми надходження ===");

        Console.WriteLine("Оберіть номенклатуру:");

        foreach (Item item in items)
        {
            Console.WriteLine($"{item.Code}. {item.Name}, {item.Unit}, {item.Group}");
        }

        Console.WriteLine();

        Console.Write("Введіть Code номенклатури: ");
        string itemId = Console.ReadLine()!;

        Item? selectedItem = null;

        foreach (Item item in items)
        {
            if (item.Code == itemId)
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
            Console.WriteLine($"{account.Code}. {account.Name}");
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
            Console.WriteLine($"{item.Code}. {item.Name}, {item.Unit}, {item.Group}");
        };
        Console.WriteLine();
        Console.WriteLine("=== Склади ===");

        foreach (Warehouse warehouse in warehouses)
        {
            Console.WriteLine($"{warehouse.Code}, {warehouse.Name}, {warehouse.Type}");
        };
        Console.WriteLine();
        Console.WriteLine("=== Контрагенти ===");

        foreach (Counterparty counterparty in counterparties)
        {
            Console.WriteLine($"{counterparty.Code}. {counterparty.Name}, {counterparty.Type}, {counterparty.TaxNumber}");
        };
        Console.WriteLine();
        Console.WriteLine("=== Рахунки обліку ===");

        foreach (Account account in accounts)
        {
            Console.WriteLine($"{account.Code}. {account.Name}");
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
            Console.WriteLine($"{counterparty.Code}. {counterparty.Name}, {counterparty.Type}, {counterparty.TaxNumber}");
        }

        string supplierId = Console.ReadLine()!;

        Counterparty? selectedSupplier = null;

        foreach (Counterparty counteraparty in counterparties)
        {
            if (counteraparty.Code == supplierId)
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
                Console.WriteLine($"{warehouse.Code}. {warehouse.Name}, {warehouse.Type}");
            }

            string warehouseId = Console.ReadLine()!;

            Warehouse? selectedWarehouse = null;

            foreach (Warehouse warehouse in warehouses)
            {
                if (warehouse.Code == warehouseId)
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
        Console.WriteLine("=== Поступлення сировини ===");

        if (receiptDocuments.Count == 0)
        {
            Console.WriteLine("Документів поки немає.");
        }
        else
        {
            foreach (ReceiptDocument document in receiptDocuments)
            {
                Console.WriteLine($"{document.Id}. {document.Number} від {document.Date:dd.MM.yyyy}");
                Console.WriteLine($"   Статус: {document.Status}");
                Console.WriteLine($"   Постачальник: {document.Supplier.Name}");
                Console.WriteLine($"   Склад: {document.Warehouse.Name}");

                if (document.Lines.Count == 0)
                {
                    Console.WriteLine("   Рядків поки немає.");
                }
                else
                {
                    Console.WriteLine("   Рядки:");

                    foreach (ReceiptLine line in document.Lines)
                    {
                        Console.WriteLine($"   {line.Id}. {line.Item.Name}, партія {line.BatchNumber}, {line.Quantity} {line.Item.Unit} × {line.Price} = {line.GetAmount()}");
                    }

                    Console.WriteLine($"   Загальна сума: {document.GetTotalAmount()}");
                }

                Console.WriteLine();
            }
        }
    }
    else if (choice == "12")
    {
        Console.WriteLine("=== Додавання рядка в документ надходження ===");

        if (receiptDocuments.Count == 0)
        {
            Console.WriteLine("Спочатку створіть документ надходження сировини.");
        }
        else
        {
            Console.WriteLine("Оберіть документ надходження:");

            foreach (ReceiptDocument document in receiptDocuments)
            {
                Console.WriteLine($"{document.Id}. {document.Number} від {document.Date:dd.MM.yyyy}");
                Console.WriteLine($"   Постачальник: {document.Supplier.Name}");
                Console.WriteLine($"   Склад: {document.Warehouse.Name}");
            }
            int documentId = int.Parse(Console.ReadLine()!);

            ReceiptDocument? selectedDocument = null;

            foreach (ReceiptDocument document in receiptDocuments)
            {
                if (document.Id == documentId)
                {
                    selectedDocument = document;
                }
            }

            if (selectedDocument == null)
            {
                Console.WriteLine("Документ з таким Id не знайдено.");
            }
            else if (selectedDocument.Status != DocumentStatus.Draft)
            {
                Console.WriteLine("Рядки можна додавати лише в документ зі статусом Draft.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Оберіть номенклатуру:");

                foreach (Item item in items)
                {
                    Console.WriteLine($"{item.Code}. {item.Name}, {item.Unit}, {item.Group}");
                }

                Console.WriteLine();

                Console.Write("Введіть Id номенклатури: ");
                string itemId = Console.ReadLine()!;

                Item? selectedItem = null;

                foreach (Item item in items)
                {
                    if (item.Code == itemId)
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
                    Console.Write("Введіть номер партії: ");
                    string batchNumber = Console.ReadLine()!;

                    Console.Write("Введіть кількість: ");
                    decimal quantity = decimal.Parse(
                        Console.ReadLine()!.Replace(",", "."),
                        CultureInfo.InvariantCulture
                    );

                    Console.Write("Введіть ціну без ПДВ: ");
                    decimal price = decimal.Parse(
                        Console.ReadLine()!.Replace(",", "."),
                        CultureInfo.InvariantCulture
                    );

                    if (string.IsNullOrWhiteSpace(batchNumber))
                    {
                        Console.WriteLine("Номер партії не може бути порожнім.");
                    }
                    else if (quantity <= 0)
                    {
                        Console.WriteLine("Кількість повинна бути більше нуля.");
                    }
                    else if (price <= 0)
                    {
                        Console.WriteLine("Ціна повинна бути більше нуля.");
                    }
                    else
                    {
                        decimal amount = quantity * price;

                        int nextLineId = selectedDocument.Lines.Count + 1;

                        ReceiptLine line = new ReceiptLine
                        {
                            Id = nextLineId,
                            Item = selectedItem,
                            BatchNumber = batchNumber,
                            Quantity = quantity,
                            Price = price
                        };

                        selectedDocument.Lines.Add(line);

                        Console.WriteLine();
                        Console.WriteLine("Рядок додано до документа.");
                        Console.WriteLine($"Документ: {selectedDocument.Number}");
                        Console.WriteLine($"Номенклатура: {line.Item.Name}");
                        Console.WriteLine($"Партія: {line.BatchNumber}");
                        Console.WriteLine($"Кількість: {line.Quantity} {line.Item.Unit}");
                        Console.WriteLine($"Ціна: {line.Price}");
                        Console.WriteLine($"Сума: {line.GetAmount()}");
                    }
                }
            }
        }

}
    else if (choice == "13")
    {
        Console.WriteLine("=== Підсумок поступлення ===");

        if (receiptDocuments.Count == 0)
        {
            Console.WriteLine("Документів поки немає.");
        }
        else
        {
            Console.WriteLine("Оберіть документ:");

            foreach (ReceiptDocument document in receiptDocuments)
            {
                Console.WriteLine($"{document.Id}. {document.Number} від {document.Date:dd.MM.yyyy}");
            }

            Console.WriteLine();

            Console.Write("Введіть Id документа: ");
            int documentId = int.Parse(Console.ReadLine()!);

            ReceiptDocument? selectedDocument = null;

            foreach (ReceiptDocument document in receiptDocuments)
            {
                if (document.Id == documentId)
                {
                    selectedDocument = document;
                }
            }

            if (selectedDocument == null)
            {
                Console.WriteLine("Документ з таким Id не знайдено.");
            }
            else if (selectedDocument.Lines.Count == 0)
            {
                Console.WriteLine("У документа немає рядків.");
            }
            else
            {
                decimal totalAmount = selectedDocument.GetTotalAmount();

                VatRate selectedVatRate = vatRates[0];

                decimal vatAmount = totalAmount * selectedVatRate.RatePercent / 100;
                decimal totalWithVat = totalAmount + vatAmount;

                Console.WriteLine();
                Console.WriteLine($"Документ: {selectedDocument.Number} від {selectedDocument.Date:dd.MM.yyyy}");
                Console.WriteLine($"Постачальник: {selectedDocument.Supplier.Name}");
                Console.WriteLine($"Склад: {selectedDocument.Warehouse.Name}");
                Console.WriteLine($"Кількість рядків: {selectedDocument.Lines.Count}");
                Console.WriteLine($"Сума без ПДВ: {totalAmount}");
                Console.WriteLine($"Ставка ПДВ: {selectedVatRate.Name}");
                Console.WriteLine($"Сума ПДВ: {vatAmount}");
                Console.WriteLine($"Сума з ПДВ: {totalWithVat}");
            }
        }
    }
    else if (choice == "14")
    {
        Console.WriteLine("=== Перевірка надходження ===");
        if (receiptDocuments.Count == 0)
        {
            Console.WriteLine("Документів поки немає.");
        }
        else
        {
            Console.WriteLine("Оберіть документ:");
            foreach (ReceiptDocument document in receiptDocuments)
            {
                Console.WriteLine($"{document.Id}. {document.Number} від {document.Date:dd.MM.yyyy}");
            }
            Console.WriteLine();
            Console.Write("Введіть Id документа: ");
            int documentId = int.Parse(Console.ReadLine()!);
            ReceiptDocument? selectedDocument = null;
            foreach (ReceiptDocument document in receiptDocuments)
            {
                if (document.Id == documentId)
                {
                    selectedDocument = document;
                }
            }
            if (selectedDocument == null)
            {
                Console.WriteLine("Документ з таким Id не знайдено.");
            }
            else
            {
                List<string> errors = selectedDocument.Validate();
                if (errors.Count == 0)
                {
                    Console.WriteLine("Документ пройшов перевірку без помилок.");
                }
                else
                {
                    Console.WriteLine("Документ має наступні помилки:");
                    foreach (string error in errors)
                    {
                        Console.WriteLine($"- {error}");
                    }
                }
            }
        }
    }
    else if (choice == "15")
    {
        Console.WriteLine("=== Проведення документа ===");

        if (receiptDocuments.Count == 0)
        {
            Console.WriteLine("Документів поки немає.");
        }

        else
        {
            Console.WriteLine("Оберіть документ:");

            foreach (ReceiptDocument document in receiptDocuments)
            {
                Console.WriteLine($"{document.Id}. {document.Number} від {document.Date:dd.MM.yyyy}, статус: {document.Status}");
            }

            Console.WriteLine();

            Console.Write("Введіть Id документа: ");
            int documentId = int.Parse(Console.ReadLine()!);

            ReceiptDocument? selectedDocument = null;

            foreach (ReceiptDocument document in receiptDocuments)
            {
                if (document.Id == documentId)
                {
                    selectedDocument = document;
                }
            }

            if (selectedDocument == null)
            {
                Console.WriteLine("Документ з таким Id не знайдено.");
            }
            else if (selectedDocument.Status != DocumentStatus.Draft)
            {
                Console.WriteLine("Документ не можна провести повторно.");
                Console.WriteLine($"Поточний статус документа: {selectedDocument.Status}");
            }
            else
            {
                PostingService postingService = new PostingService();

                PostingResult postingResult = postingService.PostReceipt(selectedDocument);

                if (!postingResult.IsSuccess)
                {
                    Console.WriteLine("Документ не можна провести, бо він має помилки:");

                    foreach (string error in postingResult.Errors)
                    {
                        Console.WriteLine($"- {error}");
                    }
                }
                else
                {
                    foreach (InventoryMovement movement in postingResult.InventoryMovements)
                    {
                        movement.Id = inventoryMovements.Count + 1;
                        inventoryMovements.Add(movement);
                    }

                    foreach (AccountingEntry entry in postingResult.AccountingEntries)
                    {
                        entry.Id = accountingEntries.Count + 1;
                        accountingEntries.Add(entry);
                    }

                    selectedDocument.Status = DocumentStatus.Posted;

                    Console.WriteLine("Документ проведено.");
                    Console.WriteLine($"Створено складських рухів: {postingResult.InventoryMovements.Count}");
                    Console.WriteLine($"Створено проводок: {postingResult.AccountingEntries.Count}");
                }
            }
        }
    }
    else if (choice == "16")
    {
        Console.WriteLine("=== Скасування поступлення ===");

        if (receiptDocuments.Count == 0)
        {
            Console.WriteLine("Документів поки немає.");
        }
        else
        {
            Console.WriteLine("Оберіть документ:");

            foreach (ReceiptDocument document in receiptDocuments)
            {
                Console.WriteLine($"{document.Id}. {document.Number} від {document.Date:dd.MM.yyyy}, статус: {document.Status}");
            }

            Console.WriteLine();

            Console.Write("Введіть Id документа: ");
            int documentId = int.Parse(Console.ReadLine()!);

            ReceiptDocument? selectedDocument = null;

            foreach (ReceiptDocument document in receiptDocuments)
            {
                if (document.Id == documentId)
                {
                    selectedDocument = document;
                }
            }

            if (selectedDocument == null)
            {
                Console.WriteLine("Документ з таким Id не знайдено.");
            }
            else if (selectedDocument.Status == DocumentStatus.Cancelled)
            {
                Console.WriteLine("Документ вже скасовано.");
            }
            else
            {
                selectedDocument.Status = DocumentStatus.Cancelled;

                Console.WriteLine("Документ скасовано.");
                Console.WriteLine("Увага: сторнування рухів ще не робимо. Це буде пізніше.");
            }
        }
    }
    else if (choice == "17")
    {
        Console.WriteLine("=== Проводки ===");

        if (accountingEntries.Count == 0)
        {
            Console.WriteLine("Проводок поки немає.");
        }
        else
        {
            foreach (AccountingEntry entry in accountingEntries)
            {
                Console.WriteLine(
                    $"{entry.Id}. {entry.Date:dd.MM.yyyy} " +
                    $"Дт {entry.DebitAccount.Code} " +
                    $"Кт {entry.CreditAccount.Code} — " +
                    $"{entry.Amount} — " +
                    $"{entry.Description}"
                );
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
           