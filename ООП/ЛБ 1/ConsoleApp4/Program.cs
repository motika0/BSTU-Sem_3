using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main()
        {
                // Задание кортежа
                var cort = (42, "Hello", 'A', "World", 12345678901234567890UL);

                // b. Вывод кортежа целиком
                Console.WriteLine($"Кортеж: {cort}");

                // Вывод выборочно (элементы 1, 3, 4)
                Console.WriteLine($"1 элемент: {cort.Item1}, 3 элемент: {cort.Item3}, 4 элемент: {cort.Item4}");

                // c. Распаковка кортежа в переменные
                (int num, string str, char let, string mesto, ulong bignum) = cort;

                // Демонстрация различных способов распаковки
                Console.WriteLine($"Распаковка: {num}, {str}, {let}, {mesto}, {bignum}");

                // Использование переменной _
                var (_, sms, _, _, _) = cort; // игнорируем остальные элементы
                Console.WriteLine($"Использование: {sms}");

                // d. Сравнение двух кортежей
                var cort2 = (42, "Hello", 'A', "World", 12345678901234567890UL);
                bool ravno = cort.Equals(cort2);
                Console.WriteLine($"Сравнение двух кортежей: {ravno}");
            }
        }

    }

