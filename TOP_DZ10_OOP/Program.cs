using System;
using System.Collections.Generic;

public interface ICommand
{
    void Execute();
}

public class Light
{
    public void TurnOn()
    {
        Console.WriteLine("Свет включен.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Свет выключен.");
    }
}

public class Stereo
{
    public void TurnOn()
    {
        Console.WriteLine("Стереосистема включена.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Стереосистема выключена.");
    }

    public void SetCd()
    {
        Console.WriteLine("Установлен режим CD.");
    }

    public void SetVolume(int volume)
    {
        Console.WriteLine($"Громкость установлена на {volume}.");
    }
}

public class LightOnCommand : ICommand
{
    private Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOn();
    }
}

public class LightOffCommand : ICommand
{
    private Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOff();
    }
}

public class StereoOnWithCDCommand : ICommand
{
    private Stereo _stereo;

    public StereoOnWithCDCommand(Stereo stereo)
    {
        _stereo = stereo;
    }

    public void Execute()
    {
        _stereo.TurnOn();
        _stereo.SetCd();
        _stereo.SetVolume(10);
    }
}

public class RemoteControl
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        if (_command != null)
        {
            _command.Execute();
        }
        else
        {
            Console.WriteLine("Кнопка не запрограммирована.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Настройка пульта управления ---");

        var light = new Light();
        var stereo = new Stereo();

        ICommand lightOn = new LightOnCommand(light);
        ICommand lightOff = new LightOffCommand(light);
        ICommand stereoOnCd = new StereoOnWithCDCommand(stereo);

        var remote = new RemoteControl();

        Console.WriteLine("\nПрограммируем кнопку на включение света...");
        remote.SetCommand(lightOn);

        Console.WriteLine("Нажимаем кнопку...");
        remote.PressButton();

        Console.WriteLine("\nПрограммируем кнопку на включение стереосистемы...");
        remote.SetCommand(stereoOnCd);

        Console.WriteLine("Нажимаем кнопку...");
        remote.PressButton();

        Console.WriteLine("\nПрограммируем кнопку на выключение света...");
        remote.SetCommand(lightOff);

        Console.WriteLine("Нажимаем кнопку...");
        remote.PressButton();

        Console.ReadLine();
    }
}

