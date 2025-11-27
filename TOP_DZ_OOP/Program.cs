using System;
using System.Collections.Generic;

public sealed class CacheService
{
    private static readonly CacheService _instance = new CacheService();

    private static Dictionary<string, object> _cache;
    
    public void Add(string key, object value)
    {
        _cache[key] = value;
        Console.WriteLine($"Данные '{key}' добавлены.");

    }

    public object? Get(string key)
    {
        return _cache[key];
    }
    public static CacheService Instance
    {
        get { return _instance; }
    }
    private CacheService()
    {
        _cache = new Dictionary<string, object>();
        Console.WriteLine("(Экземпляр CacheService создан)");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        
        Console.WriteLine("--- Демонстрация работы глобального кэша (Singleton) ---");

        CacheService cache1 = CacheService.Instance;
        CacheService cache2 = CacheService.Instance;

        Console.WriteLine("\nДобавляем данные в кэш через первую ссылку...");
        cache1.Add("ConnectionString", "Server=.;Database=CacheDB;");
        cache1.Add("ApiKey", "XYZ12345ABC");

        Console.WriteLine("\nПолучаем данные из кэша через ВТОРУЮ ссылку...");
        Console.WriteLine($"Значение по ключу 'ConnectionString': {cache2.Get("ConnectionString")}");
        Console.WriteLine($"Значение по ключу 'ApiKey': {cache2.Get("ApiKey")}");

        Console.WriteLine("\nПроверяем, что обе переменные ссылаются на один объект...");
        Console.WriteLine($"Результат: {object.ReferenceEquals(cache1, cache2)}");
    }
}