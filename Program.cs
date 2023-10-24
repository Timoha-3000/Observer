using System;
using System.Collections.Generic;

// Интерфейс, который должны реализовать все наблюдатели
public interface IObserver
{
    void Update();
}

// Интерфейс издателя (Subject)
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}

// Класс Деканата, является издателем
public class DeanOffice : ISubject
{
    private List<IObserver> observers = new List<IObserver>();

    private bool isProgressReportCreated = false;

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.Update();
        }
    }

    public void CreateProgressReport()
    {
        // Логика создания текущей успеваемости
        isProgressReportCreated = true;
        Console.WriteLine("Текущая успеваемость создана.");

        Notify(); // Уведомляем всех наблюдателей
    }

    public bool IsProgressReportCreated()
    {
        return isProgressReportCreated;
    }
}

// Класс Кафедры, является наблюдателем
public class Department : IObserver
{
    private string departmentName;
    private DeanOffice deanOffice;

    public Department(string name, DeanOffice deanOffice)
    {
        departmentName = name;
        this.deanOffice = deanOffice;
        deanOffice.Attach(this);
    }

    public void Update()
    {
        if (!deanOffice.IsProgressReportCreated())
        {
            Console.WriteLine($"Внимание! Кафедра {departmentName} не получила текущую успеваемость.");
        }
        else
        {
            Console.WriteLine($"Кафедра {departmentName} получила текущую успеваемость.");
        }
    }
}

// Пример использования
public class Program
{
    public static void Main(string[] args)
    {
        DeanOffice deanOffice = new DeanOffice();

        // Создание кафедр и привязка их к деканату
        Department department1 = new Department("Кафедра 1", deanOffice);
        Department department2 = new Department("Кафедра 2", deanOffice);

        // Преподаватель создает текущую успеваемость
        deanOffice.CreateProgressReport();
    }
}