using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNamespace
{
    // 1A: Enum с днями недели
    public enum DayOfWeekEnum
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    // 1B: Класс для работы с массивом и подсчетом суммы
    public class ArrayOperations
    {
        public static int Sum(int[] array)
        {
            int total = 0;
            foreach (var number in array)
            {
                total += number;
            }
            return total;
        }
    }

    // 2: Класс Комп с полями и интерфейсом IComparable
    public class Computer : IComparable<Computer>
    {
        public string Model { get; set; }
        public int RAM { get; set; } // в ГБ
        public int Storage { get; set; } // в ГБ

        public Computer(string model, int ram, int storage)
        {
            Model = model;
            RAM = ram;
            Storage = storage;
        }

        public int CompareTo(Computer other)
        {
            return this.RAM.CompareTo(other.RAM); // Сравниваем по RAM
        }
    }

    // 3: Метод расширения для типа bool
    public static class BoolExtensions
    {
        public static bool ToTrueFalse(this bool value)
        {
            return value ? true : false; // Простой метод, возвращающий true или false
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1A: Вывод дней недели
            Console.WriteLine("Дни недели:");
            foreach (DayOfWeekEnum day in Enum.GetValues(typeof(DayOfWeekEnum)))
            {
                Console.WriteLine(day);
            }

            // Задание 1B: Работа с массивом
            int[] numbers = { 1, 2, 3, 4, 5 };
            int sum = ArrayOperations.Sum(numbers);
            Console.WriteLine($"Сумма элементов массива: {sum}");

            // Задание 2: Создание объектов Комп
            Computer comp1 = new Computer("Dell", 16, 512);
            Computer comp2 = new Computer("HP", 8, 256);

            Console.WriteLine($"Компьютер 1: {comp1.Model}, RAM: {comp1.RAM} ГБ, Storage: {comp1.Storage} ГБ");
            Console.WriteLine($"Компьютер 2: {comp2.Model}, RAM: {comp2.RAM} ГБ, Storage: {comp2.Storage} ГБ");

            // Сравнение компьютеров
            if (comp1.CompareTo(comp2) > 0)
            {
                Console.WriteLine($"{comp1.Model} имеет больше RAM, чем {comp2.Model}.");
            }
            else if (comp1.CompareTo(comp2) < 0)
            {
                Console.WriteLine($"{comp2.Model} имеет больше RAM, чем {comp1.Model}.");
            }
            else
            {
                Console.WriteLine($"{comp1.Model} и {comp2.Model} имеют одинаковое количество RAM.");
            }

            // Задание 3: Использование метода расширения
            bool testValue = true;
            Console.WriteLine($"Результат метода расширения: {testValue.ToTrueFalse()}");
        }
    }
}