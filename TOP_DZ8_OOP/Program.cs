using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

public record Product(string Name, decimal Price);

public interface IExportStrategy
{
    void Export(IEnumerable<Product> data);
}

public class CsvExportStrategy : IExportStrategy
{
    public void Export(IEnumerable<Product> data)
    {
        Console.WriteLine("Экспортирую данные (CSV)...");
        Console.WriteLine("Name,Price");

        foreach (var item in data)
        {
            Console.WriteLine($"{item.Name},{item.Price}");
        }
    }
}
public class JsonExportStrategy : IExportStrategy
{
    public void Export(IEnumerable<Product> data)
    {
        Console.WriteLine("Экспортирую данные (JSON)...");

        string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

        Console.WriteLine(jsonString);
    }
}

public class DataExporter
{
    private IExportStrategy _strategy;

    public DataExporter(IExportStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(IExportStrategy strategy)
    {
        _strategy = strategy;
    }

    public void ExportData(IEnumerable<Product> data)
    {
        if (_strategy == null)
        {
            Console.WriteLine("Стратегия не выбрана.");
            return;
        }
        _strategy.Export(data);
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Универсальный экспортер данных ---");

        var products = new List<Product>
            {
                new Product("Ноутбук", 1200),
                new Product("Мышь", 25)
            };

        Console.WriteLine("Данные для экспорта:");
        foreach (var p in products)
        {
            Console.WriteLine($"- Продукт: {p.Name}, Цена: {p.Price}");
        }

        DataExporter exporter = new DataExporter(new CsvExportStrategy());

        Console.WriteLine("\nЭкспорт в формате CSV:");
        exporter.ExportData(products);

        Console.WriteLine("\n--- Меняем стратегию на JSON ---\n");

        exporter.SetStrategy(new JsonExportStrategy());

        Console.WriteLine("Экспорт в формате JSON:");
        exporter.ExportData(products);
    }
}
