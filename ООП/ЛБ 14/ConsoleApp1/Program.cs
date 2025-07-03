using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

class Program
{
    private static int n;

    static void Main()
    {
        // Информация о процессах
        foreach (Process process in Process.GetProcesses())
        {
            try
            {
                Console.WriteLine($"ID: {process.Id}, Name: {process.ProcessName}, Priority: {process.BasePriority}, Start Time: {process.StartTime}, Total Processor Time: {process.TotalProcessorTime}, Responding: {process.Responding}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Информация об текущем домене и сборках
        AppDomain currentDomain = AppDomain.CurrentDomain;
        Console.WriteLine($"Current Domain: {currentDomain.FriendlyName}");
        foreach (Assembly assembly in currentDomain.GetAssemblies())
        {
            Console.WriteLine($"Assembly: {assembly.FullName}");
        }

        // Ввод значения n
        Console.Write("Введите n: ");
        n = int.Parse(Console.ReadLine());

        // Поток для вычисления простых чисел
        Thread primeThread = new Thread(CalculatePrimes);
        primeThread.Start();
        primeThread.Join();

        // Сначала выводим все чётные числа
        PrintEvenNumbers();
        // Затем выводим все нечётные числа
        PrintOddNumbers();

        // Пауза между выводами
        Console.WriteLine("-----");

        // Затем выводим числа поочерёдно
        PrintAlternatingNumbers();

        // Таймер для повторяющегося сообщения
        Timer timer = new Timer(PrintMessage, null, 0, 2000);
        Console.ReadLine();
    }

    static void CalculatePrimes()
    {
        using (StreamWriter writer = new StreamWriter("primes.txt"))
        {
            for (int i = 2; i <= n; i++)
            {
                if (IsPrime(i))
                {
                    Console.WriteLine(i);
                    writer.WriteLine(i);
                    Thread.Sleep(100); // Имитация времени обработки
                }
            }
        }
    }

    static bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

    static void PrintEvenNumbers()
    {
        Console.WriteLine("Чётные числа:");
        for (int i = 2; i <= n; i += 2) // Вывод чётных чисел
        {
            Console.WriteLine(i);
            Thread.Sleep(50); // Имитация разной скорости
        }
    }

    static void PrintOddNumbers()
    {
        Console.WriteLine("Нечётные числа:");
        for (int i = 1; i <= n; i += 2) // Вывод нечётных чисел
        {
            Console.WriteLine(i);
            Thread.Sleep(50); // Имитация разной скорости
        }
    }

    static void PrintAlternatingNumbers()
    {
        Console.WriteLine("Чередование чётных и нечётных чисел:");
        for (int i = 2, j = 1; i <= n || j <= n; i += 2, j += 2)
        {
            if (i <= n) Console.WriteLine(i); // Чётное число
            if (j <= n) Console.WriteLine(j); // Нечётное число
        }
    }

    static void PrintMessage(object state)
    {
        Console.WriteLine("Повторяющееся сообщение: " + DateTime.Now);
    }
}