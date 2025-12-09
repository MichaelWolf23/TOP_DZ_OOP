using System;
using System.Collections.Generic;

public delegate void PriceChangedHandler(decimal newPrice, string bidderName, AuctionLot source);
public class Bidder
{
    public string Name { get; set; }
    public void OnPriceChanged(decimal newPrice, string bidderName, AuctionLot source)
    {
        if (bidderName != this.Name)
        {
            Console.WriteLine($" [{this.Name}]: Новая ставка на '{source.Name}' - {newPrice:F2} (от {bidderName}).");
        }
    }
    public Bidder(string name)
    {
        Name = name;
    }
}

public class AuctionLot
{
    public string Name { get; set; }
    public decimal CurrentPrice { get; private set; }

    public event PriceChangedHandler PriceChanged;

    public AuctionLot(string name, decimal currentPrice)
    {
        Name = name;
        CurrentPrice = currentPrice;
        Console.WriteLine($"--- Аукцион начинается! Лот: '{Name}'. Начальная цена: {currentPrice:F2} ---");

    }

    public void PlaceBid(Bidder bidder, decimal newPrice)
    {
        Console.WriteLine($"\n{bidder.Name} делает ставку: {newPrice:F2}");

        if (newPrice > CurrentPrice)
        {
            CurrentPrice = newPrice;
            OnPriceChanged(newPrice, bidder.Name);
        }
        else
        {
            Console.WriteLine($"\n Ставка не принята. Сумма должна быть выше {CurrentPrice:F2}.");
        }
    }

    protected virtual void OnPriceChanged(decimal newPrice, string bidderName)
    {
        PriceChanged?.Invoke(newPrice, bidderName, this);
    }
    public void Subscribe(Bidder bidder)
    {
        PriceChanged += bidder.OnPriceChanged;
        Console.WriteLine($"Участник '{bidder.Name}' подписался на лот.");
    }

    public void Unsubscribe(Bidder bidder)
    {
        PriceChanged -= bidder.OnPriceChanged;
        Console.WriteLine($"Участник '{bidder.Name}' отписался от лота.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var vase = new AuctionLot("Старинная ваза", 1000.00m);

        var ivan = new Bidder("Иван");
        var petr = new Bidder("Петр");
        var anna = new Bidder("Анна");

        vase.Subscribe(ivan);
        vase.Subscribe(petr);
        vase.Subscribe(anna);

        vase.PlaceBid(ivan, 1200.00m);
        vase.PlaceBid(anna, 1500.00m);
        vase.PlaceBid(petr, 1300.00m);

        Console.WriteLine("\n--- ОТПИСЫВАЕМ ПЕТРА ---\n");

        vase.Unsubscribe(petr);

        vase.PlaceBid(ivan, 1700.00m);
        vase.PlaceBid(anna, 1800.00m);
        vase.PlaceBid(petr, 2500.00m);
    }
}