using System;
using System.Collections.Generic;

public interface IMessageSender
{
    void Send(string message);
}

public class EmailSender : IMessageSender
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка по Email: {message}");
    }
}

public class SmsSender : IMessageSender
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка по SMS: {message}");
    }
}

public abstract class NotificationServiceFactory
{
    public abstract IMessageSender CreateSender();

}

public class EmailNotificationFactory : NotificationServiceFactory
{
    public override IMessageSender CreateSender()
    {
        return new EmailSender();
    }
}

public class SmsNotificationFactory : NotificationServiceFactory
{
    public override IMessageSender CreateSender()
    {
        return new SmsSender();
    }
}

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("--- Гибкая система уведомлений ---");
        Console.Write("Какой тип уведомлений использовать? (email/sms): ");

        string input = Console.ReadLine()?.ToLower();

        NotificationServiceFactory factory;

        switch (input)
        {
            case "email":
                factory = new EmailNotificationFactory();
                Console.WriteLine("\nСоздана фабрика для Email.");
                break;
            case "sms":
                factory = new SmsNotificationFactory();
                Console.WriteLine("\nСоздана фабрика для SMS.");
                break;
            default:
                Console.WriteLine("\nНеизвестный тип уведомлений.");
                return;
        }

        Console.WriteLine("Отправляем уведомление...");


        IMessageSender sender = factory.CreateSender();
        sender.Send("Ваш заказ #123 успешно оформлен.");

        Console.WriteLine("\n------------------------------------");

        Console.ReadLine();
    }
}
