using System;
using System.Collections.Generic;

public class Employee
{
    public string Name { get; set; }
    public virtual string Position { get; set; }
    public decimal BaseSalary { get; set; }
    public virtual decimal CalculateMonthlySalary() => BaseSalary;
    public virtual void PrintMonthlySalary()
    {
        Console.WriteLine($"Зарплата для {Position} {Name}: {CalculateMonthlySalary()}");
    }

    public Employee(string name, decimal baseSalary)
    {
        Name = name;
        BaseSalary = baseSalary;
        Position = "Работник";
    }
}

public class Manager : Employee
{
    public decimal Bonus { get; set; }
    public override decimal CalculateMonthlySalary() => BaseSalary + Bonus;

    public Manager(string name, decimal baseSalary, decimal bonus) : base(name, baseSalary)
    {
        Bonus = bonus;
        Position = "Менеджер";
    }
}

public class Developer : Employee
{
    public int LinesOfCodeWritten { get; set; }
    public override decimal CalculateMonthlySalary() => BaseSalary + (LinesOfCodeWritten * 10m);

    public Developer(string name, decimal baseSalary, int linesOfCodeWritten) : base(name, baseSalary)
    {
        LinesOfCodeWritten = linesOfCodeWritten;
        Position = "Разработчик";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Employee> employees = new List<Employee>()
        {
            new Manager("Иван Петров", 10000m, 2000m),
            new Developer("Анна Сидорова", 15000m, 1359),
            new Manager("Олег Васильев", 14000m, 5000m),
            new Developer("Петр Сидоров", 110000m, 1623)
        };

        foreach (Employee employee in employees)
        {
            employee.PrintMonthlySalary();
        }
    }
}