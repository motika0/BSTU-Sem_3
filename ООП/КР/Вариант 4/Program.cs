using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// 1A: Проверка, содержит ли одна строка другую
public class StringOperations
{
    public static bool ContainsSubstring(string mainString, string substring)
    {
        return mainString.Contains(substring);
    }
}

// 1B: Работа с одномерным массивом строк
public class ArrayOperations
{
    public static string[] ReverseArray(string[] array)
    {
        Array.Reverse(array);
        return array;
    }
}

// 2: Класс Day с индексатором и переопределением методов
public class Day
{
    private string[] daysOfWeek =
    {
        "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
    };

    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= daysOfWeek.Length)
            {
                throw new IndexOutOfRangeException("Индекс должен быть в пределах от 0 до 6.");
            }
            return daysOfWeek[index];
        }
    }

    public override string ToString()
    {
        return string.Join(", ", daysOfWeek);
    }

    public override bool Equals(object obj)
    {
        if (obj is Day other)
        {
            return string.Join(",", daysOfWeek) == string.Join(",", other.daysOfWeek);
        }
        return false;
    }
}

// 3: Интерфейс IMove, абстрактный класс Transport, класс Car
public interface IMove
{
    void Move();
}

public abstract class Transport
{
    public abstract void Move();
}

public class Car : Transport, IMove
{
    public override void Move()
    {
        Console.WriteLine("Car is moving.");
    }

    void IMove.Move()
    {
        Console.WriteLine("IMove: Car is moving.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Задание 1A: Проверка содержимого строк
        Console.Write("Введите основную строку: ");
        string mainString = Console.ReadLine();
        Console.Write("Введите подстроку для проверки: ");
        string substring = Console.ReadLine();

        bool contains = StringOperations.ContainsSubstring(mainString, substring);
        Console.WriteLine($"Строка '{mainString}' {(contains ? "содержит" : "не содержит")} подстроку '{substring}'.");

        // Задание 1B: Работа с массивом строк
        string[] stringArray = { "First", "Second", "Third", "Fourth" };
        Console.WriteLine("Исходный массив: " + string.Join(", ", stringArray));
        stringArray = ArrayOperations.ReverseArray(stringArray);
        Console.WriteLine("Обратный порядок: " + string.Join(", ", stringArray));

        // Задание 2: Индексатор в классе Day
        Day days = new Day();
        Console.Write("Введите индекс (0-6) для получения дня недели: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            try
            {
                Console.WriteLine($"День недели с индексом {index}: {days[index]}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Переопределение ToString
        Console.WriteLine("Все дни недели: " + days.ToString());

        // Задание 3: Создание объекта Car и демонстрация вызова методов
        Car myCar = new Car();
        myCar.Move(); // Вызов метода Move из Car
        ((IMove)myCar).Move(); // Вызов метода Move из IMove
    }
}
