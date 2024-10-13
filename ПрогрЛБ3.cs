//Варіант 2.
//Сформувати файл “Export.xml”, що містить інформацію про дані з полями: код; найменування
//товару; країна, що експортує товар; об’єм товару в одиницях; ціна.
// -Переглянути файл на консолі;
// -За заданим кодом Х видати найменування товару та об’єм його поставок.
// -Обчислити загальну вартість товару, який експортується в країну Y.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

class ExportItem
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public int Volume { get; set; }
    public decimal Price { get; set; }
}

class Program
{
    static void Main()
    {
        List<ExportItem> exportItems = new List<ExportItem>
        {
            new ExportItem { Code = "01", Name = "Product1", Country = "Ukraine", Volume = 100, Price = 500m},
            new ExportItem {Code = "02", Name = "Product2", Country = "Austria", Volume = 200, Price = 300m},
            new ExportItem { Code = "03", Name = "Product3", Country = "USA", Volume = 500, Price = 100m}
        };

        XElement exportXml = new XElement("Export",
            exportItems.Select(item =>
              new XElement("Item",
                new XElement("Code", item.Code),
                new XElement("Name", item.Name),
                new XElement("Country", item.Country),
                new XElement("Volume", item.Volume),
                new XElement("Price", item.Price)
            )
            )
            );

        exportXml.Save("Export.xml");
        Console.WriteLine("Файл створено");

        Console.WriteLine(exportXml);

        string searchCode = "02";
        var searchedItem = exportItems.FirstOrDefault(item => item.Code == searchCode);
        if (searchedItem != null)
        {
            Console.WriteLine($"Найменування товару за кодом {searchCode}: {searchedItem.Name}, об'єм: {searchedItem.Volume}");
        }
        else
        {
            Console.WriteLine($"Товар за кодом {searchCode} не знайдено");
        }

        string selectedCountry = "Ukraine";
        var totalCost = exportItems
            .Where(item => item.Country == selectedCountry)
            .Sum(item => item.Volume * item.Price);
        Console.WriteLine($"Загальна вартість в Україну: {totalCost}");
    }
}