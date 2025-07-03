using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main()
        {
                // Локальная функция
                (int max, int min, int sum, char bukva) ProcessArrayAndString(int[] arr, string text)
                {
                    // Находим максимальный и минимальный элементы массива
                    int maxElement = arr[0];
                    int minElement = arr[0];
                    int totalSum = 0;

                    foreach (var number in arr)
                    {
                        if (number > maxElement) maxElement = number;
                        if (number < minElement) minElement = number;
                        totalSum += number;
                    }

                    // Получаем первую букву строки
                    char bukva = string.IsNullOrEmpty(text) ? '\0' : text[0];

                    // Возвращаем кортеж с результатами
                    return (maxElement, minElement, totalSum, bukva);
                }

                // Пример массива и строки
                int[] numbers = { 10, 20, 5, 30, 15 };
                string str = "Hello";

                // Вызов локальной функции
                var result = ProcessArrayAndString(numbers, str);

                // Вывод результата
                Console.WriteLine($"Max: {result.max}, Min: {result.min}, Sum: {result.sum}, First Letter: {result.bukva}");
            }
        }

    }

