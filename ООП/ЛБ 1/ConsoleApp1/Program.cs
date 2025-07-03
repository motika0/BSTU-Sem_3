using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
     class Program
    {
        static void Main()
        {
            // a. Объявляем строковые литералы и сравниваем их
            string Literal1 = "Hello";
            string Literal2 = "Hello";
            string Literal3 = "World";

            Console.WriteLine($"Сравнение строк:");
            Console.WriteLine($"stringLiteral1 == stringLiteral2: {Literal1 == Literal2}"); // Сравнение по значению
            Console.WriteLine($"stringLiteral1 == stringLiteral3: {Literal1 == Literal3}"); // Сравнение по значению

            // b. Создание трех строк на основе String и выполнение различных операций
            string str1 = "Привет";
            string str2 = "мир";
            string str3 = "C#";

            // Сцепление строк
            string together = string.Concat(str1, " ", str2, ", ", str3);
            Console.WriteLine($"Сцепленные строки: {together}");

            // Копирование строки
            string copied = String.Copy(str1);
            Console.WriteLine($"Скопированная строка: {copied}");

            // Выделение подстроки
            string substring = str1.Substring(0, 4);
            Console.WriteLine($"Подстрока: {substring}");

            // Разделение строки на слова
            string[] words = together.Split(' ');
            Console.WriteLine("Разделенные слова:");
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            // Вставка подстроки в заданную позицию
            string into = together.Insert(7, "замечательный ");
            Console.WriteLine($"Вставленная строка: {into}");

            // Удаление заданной подстроки
            string delete = into.Remove(7, 13); // Удаляем "замечательный "
            Console.WriteLine($"После удаления подстроки: {delete}");

            // Интерполяция строк
            string interpolated = $"Строка с интерполяцией: {str1} {str2}";
            Console.WriteLine(interpolated);

            // c. Создание пустой и null строки и демонстрация использования string.IsNullOrEmpty
            string empty = "";
            string nullstr = null;

            Console.WriteLine($"Проверка на null или пустую строку:");
            Console.WriteLine($"IsNullOrEmpty(empty): {string.IsNullOrEmpty(empty)}");
            Console.WriteLine($"IsNullOrEmpty(nullstr): {string.IsNullOrEmpty(nullstr)}");

            // Другое выполнение с пустой и null строкой
            if (string.IsNullOrEmpty(empty))
            {
                Console.WriteLine("emptyString является пустой строкой.");
            }

            if (string.IsNullOrEmpty(nullstr))
            {
                Console.WriteLine("nullString является null.");
            }

            // d. Создание строки на основе StringBuilder
            StringBuilder sb = new StringBuilder();
            sb.Append("Добро ");
            sb.Append("пожаловать");

            Console.WriteLine($"Строка из StringBuilder до модификаций: {sb}");

            // Удаление символов
            sb.Remove(0, 6); // Удаляем "Добро "
            Console.WriteLine($"После удаления: {sb}");

            // Добавление символов в начало и конец строки
            sb.Insert(0, "ешкере, "); // Добавляем в начало
            sb.Append("!"); // Добавляем в конец
            Console.WriteLine($"После добавления: {sb}");
        }
    }
}
