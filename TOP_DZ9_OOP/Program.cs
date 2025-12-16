using System;
using System.Collections.Generic;

public abstract class DataProcessor
{
    protected int _dataCount;
    public void Process()
    {
        ReadData();
        ParseData();
        AnalyzeData();
        SaveReport();
    }
    protected abstract void ReadData();
    protected abstract void ParseData();
    protected virtual void AnalyzeData()
    {
        Console.WriteLine($"[Анализ]: Анализ данных... Найдено {_dataCount} записей.");
    }

    protected virtual void SaveReport()
    {
        Console.WriteLine($"[Сохранение]: Отчет сохранен в консоль. Результат: {_dataCount} записей обработано.");
    }

}

public class CsvDataProcessor : DataProcessor
{
    protected override void ReadData()
    {
        Console.WriteLine("[Чтение]: Чтение сырых данных из CSV...");
    }

    protected override void ParseData()
    {
        Console.WriteLine("[Парсинг]: Парсинг CSV данных...");
        Random random = new Random();
        _dataCount = random.Next(1, 10);
    }
}

public class XmlDataProcessor : DataProcessor
{
    protected override void ReadData()
    {
        Console.WriteLine("[Чтение]: Чтение сырых данных из XML...");
    }

    protected override void ParseData()
    {
        Console.WriteLine("[Парсинг]: Парсинг XML данных...");
        Random random = new Random();
        _dataCount = random.Next(1, 10);
    }
}

public class Program
{
    public static void Main(string[] args)
    {        
        var csvProcessor = new CsvDataProcessor();
        var xmlProcessor = new XmlDataProcessor();

        Console.WriteLine("--- Запуск обработчика CSV данных ---");
        csvProcessor.Process();

        Console.WriteLine("\n--- Запуск обработчика XML данных ---");
        xmlProcessor.Process();

        Console.ReadLine();
    }
}