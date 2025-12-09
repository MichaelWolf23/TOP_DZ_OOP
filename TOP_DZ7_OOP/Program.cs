using System;
using System.Collections.Generic;

public class OrderValidator
{
    public bool Validate(string itemName, int quantity)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Console.WriteLine("Ошибка: Название товара не может быть пустым.");
            return false;
        }
        if (quantity <= 0)
        {
            Console.WriteLine("Ошибка: Количество должно быть больше нуля.");
            return false;
        }

        Console.WriteLine("Заказ прошел валидацию.");
        return true;
    }
}
public class OrderRepository
{
    public void Save(string itemName, int quantity)
    {
        File.WriteAllText("order.txt", $"Товар: {itemName}, Количество: {quantity}");
        Console.WriteLine("Заказ сохранен в файл.");
    }
}
public class NotificationService
{
    public void SendNotification()
    {
        Console.WriteLine("Отправка email-уведомления: 'Ваш заказ принят'.");
    }
}

public class OrderProcessor
{
    private readonly OrderValidator _validator;
    private readonly OrderRepository _repository;
    private readonly NotificationService _notifier;

    public OrderProcessor(OrderValidator validator, OrderRepository repository, NotificationService notifier)
    {
        _validator = validator;
        _repository = repository;
        _notifier = notifier;
    }

    public void ProcessOrder(string itemName, int quantity)
    {
        if (!_validator.Validate(itemName, quantity)) return;
        _repository.Save(itemName, quantity);
        _notifier.SendNotification();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        OrderProcessor orderProcessor = new OrderProcessor(
            new OrderValidator(),
            new OrderRepository(),
            new NotificationService());

        Console.WriteLine("\n--- Обработка нового заказа ---");
        orderProcessor.ProcessOrder("Чайник", 3);


        Console.WriteLine("\n--- Попытка обработки некорректного заказа (отрицательное кол-во) ---");
        orderProcessor.ProcessOrder("Аэрогриль", -5);

        Console.WriteLine("\n--- Попытка обработки некорректного заказа (Пустой товар) ---");
        orderProcessor.ProcessOrder("", 5);
    }
}