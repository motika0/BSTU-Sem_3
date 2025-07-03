using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main()
        {
            // a. Создайте целый двумерный массив и выведите его на консоль в отформатированном виде (матрица)
            int[,] matrix = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

            Console.WriteLine("Двумерный массив:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j],3} "); // Форматируем вывод
                }
                Console.WriteLine();
            }

            // b. Создайте одномерный массив строк. Выведите на консоль его содержимое, длину массива.
            string[] array = { "Первый", "Второй", "Третий" };
            Console.WriteLine("\nОдномерный массив строк:");
            foreach (var str in array)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine($"Длина массива: {array.Length}");

            // Поменяйте произвольный элемент (пользователь определяет позицию и значение).
            Console.WriteLine("\nВведите индекс элемента, который хотите изменить (0 - {0}):", array.Length - 1);
            int index = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите новое значение:");
            string newv = Console.ReadLine();
            if (index >= 0 && index < array.Length)
            {
                array[index] = newv;
                Console.WriteLine($"Элемент на позиции {index} изменен на: {array[index]}");
            }
            else
            {
                Console.WriteLine("Некорректный индекс.");
            }

            Console.WriteLine("\n");
            foreach (var str in array)
            {
                Console.WriteLine(str);
            }
            // c. Создайте ступечатый (не выровненный) массив вещественных чисел
            // с 3-мя строками, в каждой из которых 2, 3 и 4 столбцов соответственно.

            double[][] matrix2 = new double[3][];
            matrix2[0] = new double[2]; // 2 столбца
            matrix2[1] = new double[3]; // 3 столбца
            matrix2[2] = new double[4]; // 4 столбца

            Console.WriteLine("\nВведите значения для ступчатого массива:");
            for (int i = 0; i < matrix2.Length; i++)
            {
                Console.WriteLine($"Строка {i + 1} (введите {matrix2[i].Length} значения):");
                for (int j = 0; j < matrix2[i].Length; j++)
                {
                    matrix2[i][j] = double.Parse(Console.ReadLine());
                }
            }

            // Вывод ступчатого массива на консоль
            Console.WriteLine("\nСтупчатый массив:");
            for (int i = 0; i < matrix2.Length; i++)
            {
                Console.WriteLine($"Строка {i + 1}: " + string.Join(", ", matrix2[i]));
            }

            // d. Создайте неявно типизированные переменные для хранения массива и строки.
            var noname = new[] { 1, 2, 3, 4, 5 };
            var nonamestr = "Неявно типизированная строка";

            Console.WriteLine($"\nНеявно типизированный массив: {string.Join(", ", noname)}");
            Console.WriteLine($"Неявно типизированная строка: {nonamestr}");
        }
    }
}
