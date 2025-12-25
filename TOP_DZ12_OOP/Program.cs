using System;
using System.Collections.Generic;

public interface INotifier
{
    void Send(string message);
}

public class Notifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка уведомления: {message}");
    }
}

public abstract class BaseDecorator : INotifier
{
    protected INotifier _wrappee;

    public BaseDecorator(INotifier notifier)
    {
        _wrappee = notifier;
    }

    public virtual void Send(string message)
    {
        _wrappee.Send(message);
    }
}

public class SmsDecorator : BaseDecorator
{
    public SmsDecorator(INotifier notifier) : base(notifier) { }

    public override void Send(string message)
    {
        base.Send(message);
        SendSms(message);
    }

    private void SendSms(string message)
    {
        Console.WriteLine($"[SMS]: {message}");
    }
}

public class TeamsDecorator : BaseDecorator
{
    public TeamsDecorator(INotifier notifier) : base(notifier) { }

    public override void Send(string message)
    {
        base.Send(message);
        SendTeamsInfo(message);
    }

    private void SendTeamsInfo(string message)
    {
        Console.WriteLine($"[MS Teams]: {message}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Простая отправка ---");

        INotifier notifier = new Notifier();
        notifier.Send("Ваш заказ #123 в обработке.");
        Console.WriteLine();

        Console.WriteLine("--- Комбинированная отправка (базовый + SMS) ---");

        INotifier smsNotifier = new SmsDecorator(new Notifier());
        smsNotifier.Send("Ваш заказ #456 отправлен.");
        Console.WriteLine();

        Console.WriteLine("--- Полная отправка (базовый + SMS + Teams) ---");

        INotifier fullStack = new TeamsDecorator(new SmsDecorator(new Notifier()));
        fullStack.Send("Ваш заказ #789 доставлен.");
        
        Console.ReadLine();
    }
}