using System;
using System.Collections.Generic;

namespace TOP_DZ19_OOP;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public interface IUserRepository
{
    User GetUserById(int userId);
}

public interface IMessageSender
{
    void SendMessage(string recipient, string message);
}

public class NotificationService
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageSender _messageSender;

    public NotificationService(IUserRepository userRepository, IMessageSender messageSender)
    {
        _userRepository = userRepository;
        _messageSender = messageSender;
    }

    public void NotifyUser(int userId, string message)
    {
        var user = _userRepository.GetUserById(userId);
        if (user != null)
        {
            _messageSender.SendMessage(user.Email, $"Уважаемый {user.Name}, {message}");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Сервис уведомлений запущен.");
    }
}
