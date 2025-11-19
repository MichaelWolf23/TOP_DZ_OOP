public class Program
{
    public static void Main(string[] args)
    {
        Inventory inventory = new Inventory();

        Console.WriteLine("--- Управление инвентарем ---\n");

        inventory.Add(1, "Хлеб", 49.99m);
        inventory.Add(2, "Молоко", 101.99m);
        inventory.Add(3, "Сыр", 1190.0m);
        inventory.Add(4, "Картофель", 38.0m);

        Console.WriteLine("\n--- Поиск товара с ID 2 ---\n");
        inventory.Search(2);

        Console.WriteLine("\n--- Поиск товара с ID 99 ---\n");
        inventory.Search(99);
    }
}

public record Product(int Id, string Title, decimal Price);

public class Inventory
{
    private List<Product> _products = new List<Product>();

    public void Add(int id, string title, decimal price)
    {
        var product = new Product(id, title, price);
        _products.Add(product);
        Console.WriteLine($"Добавлен товар: {product}");
    }

    public void Search(int id)
    {
        try
        {
            Console.WriteLine($"Найден товар: {_products.First(product => product.Id == id)}");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"Товар с ID {id} не найден");
        }
    }
}