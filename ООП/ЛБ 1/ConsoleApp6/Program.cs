using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6 
{ 
class Program
        {
            static void Main()
            {
                fchecked();
                funchecked();
            }

            static void fchecked()
        {
                checked
                {
                    int maxValue = int.MaxValue; // Максимальное значение для int
                    Console.WriteLine($"Checked: {maxValue}");
                    // Здесь можно добавить код, который вызывает переполнение
                    // Например:
                    // int overflow = maxValue + 1; // Это вызовет исключение
                }
            }

            static void funchecked()
            {
                unchecked
                {
                    int maxValue = int.MaxValue; // Максимальное значение для int
                    Console.WriteLine($"Unchecked: {maxValue}");
                    int overflow = maxValue + 1; // Это не вызовет исключение, а просто обнулит значение
                    Console.WriteLine($"Overflowed value: {overflow}");
                }
            }
        }
    }
